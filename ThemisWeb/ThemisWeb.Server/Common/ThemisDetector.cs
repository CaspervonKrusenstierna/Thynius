

using Amazon.S3.Model;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using static ThemisWeb.Server.Common.ThemistTextConverter;

namespace ThemisWeb.Server.Common
{
    public struct MomentOfInterest
    {
        public string reason;
        public int Index;
    }
    public struct InputsStatistic
    {
        public string infoType;
        public string info;
    }
    public class ThemisDetectionData
    {
        public List<String> remarks;
        public List<MomentOfInterest> momentsOfInterest;
        public List<InputsStatistic> statistics;
    }
    public class ThemisDetector
    {
        IUserTextRepository _userTextRepository;
        ApplicationUser _user;
        UserText _text;

        private float editScore;
        private float averageTypeSpeed;
        private int detectionScore = 0;

        private int smallPasteCount = 0;
        private int totalExternalPasteChars = 0;
        private List<MomentOfInterest> momentsOfInterest = new List<MomentOfInterest>();
        private List<String> remarks = new List<String>();

        string RawContent = "";
        private async Task<bool> wasTextTypedByHandBefore(string currRawContent, string Text, UInt64 timePoint)
        {
            return currRawContent.Contains(Text);
            if (!currRawContent.Contains(Text))
            {
                IEnumerable<UserText> userTexts = await _userTextRepository.GetUserTexts(_user);
                bool wasTypedInOtherDoc = false;
                foreach(UserText text in userTexts) //TODO: MAKE A CHECK ON CLIENT SIDE IF MULTIPLE DOCS WERE OPEN TO FILTER OUT MOST OF THESE CHECKS AS THEY VERY HEAVY
                {
                    if(text.Id == _text.Id)
                    {
                        continue;
                    }
                    GetObjectResponse currTextObjResponse = await _userTextRepository.S3GetInputDataAsync(text);
                    string currTextRawAtTimePoint = GetInputsRawTextAtTimePoint(ReadInputsStream(currTextObjResponse.ResponseStream).ToList(), timePoint);
                    if (currTextRawAtTimePoint.Contains(Text))
                    {
                        wasTypedInOtherDoc = true;
                    }
                }
                return wasTypedInOtherDoc;
            }
            return true;
        }
        private async Task<bool> AnalyzeInputs(List<Input> inputs)
        {
            int previousAddCharRelativeTime = 0;
            int totalAddCharTime = 0;
            int addCharAmount = 0;
            int deleteCharAmount = 0;
            int deleteSectionAmount = 0;
            int extendedTypingBreaksCount = 0;

            UInt64 currSessionRelativeTime = 0;

            for (int i = 0; inputs.Count() > i; i++)
            {
                Input input = inputs[i];
                switch (input._ActionType)
                {
                    case ActionType.ADDCHAR:
   
                        int timeDiff = (int)input.relativeTimePointMs - previousAddCharRelativeTime;
                        if (timeDiff >= 60000)
                        {
                            extendedTypingBreaksCount++;
                        }
                        else
                        {
                            addCharAmount++;
                            totalAddCharTime += timeDiff;
                        }
                        break;

                    case ActionType.DELETECHAR: deleteCharAmount++; break;

                    case ActionType.DELETESELECTION: deleteSectionAmount++; break;

                    case ActionType.PASTE:
                        if(input.ActionContent.Length <= 10) // probably just pasting in some external one word or char that user could not type on their keyboard
                        {
                            smallPasteCount++;
                            break;
                        }
                        bool wasTypedBefore = await wasTextTypedByHandBefore(RawContent, input.ActionContent, currSessionRelativeTime+input.relativeTimePointMs);
                        if (!wasTypedBefore)
                        {
                            totalExternalPasteChars += input.ActionContent.Length;
                            momentsOfInterest.Add(new MomentOfInterest{ Index=i, reason="PASTE"});
                        }
                        break;
                case ActionType.SESSIONSTART:
                    currSessionRelativeTime = input.relativeTimePointMs;
                    break;
                }
                AdvanceInput(ref RawContent, input);
            }
            if(addCharAmount == 0)
            {
                averageTypeSpeed = totalAddCharTime;
            }
            else
            {
                averageTypeSpeed = totalAddCharTime/1000/addCharAmount;
            }
            if(deleteCharAmount == 0)
            {
                editScore = 0;
            }
            else
            {
                editScore = addCharAmount / deleteCharAmount;
            }
            return true;
        }
        public int GetDetectionScore()
        {
            float score = 0;
            if(editScore <= 0.5)
            {
                if(editScore <= 0.2)
                {
                    remarks.Add("Användaren reviderade sin text ovanligt lite.");
                    score += 1;
                }
                else
                {
                    score += (float)0.5 - editScore;
                }
            }
            if(smallPasteCount >= 20)
            {
                remarks.Add("Användaren uppvisade besynnerligt beteende i form av många små inklistringar. Totalt antal små inklistringar: " + smallPasteCount);
                score += 1;
            }
            if(totalExternalPasteChars >= RawContent.Length / 10)
            {
                score += 1;
                if(totalExternalPasteChars >= RawContent.Length / 4)
                {
                    remarks.Add("Användaren har kopierat en stor del av sin text från externa källor.");
                    score += 1;
                }
                else
                {
                    remarks.Add("Användaren har kopierat delar av sin text från externa källor." + smallPasteCount);
                }
            }
            if(score >= 3)
            {
                return 3;
            }
            if(score >= 2)
            {
                return 2;
            }
            if(score >= 1)
            {
                return 1;
            }
            return 0;
        }
        private List<InputsStatistic> GetInputsStatistics()
        {
            List<InputsStatistic> toReturn = new List<InputsStatistic>();
            toReturn.Add(new InputsStatistic { infoType = "Genomsnittlig skrivhastighet", info = averageTypeSpeed.ToString() });
            toReturn.Add(new InputsStatistic { infoType = "Ändrings poäng", info = editScore.ToString() });
            return toReturn;
        }
        public ThemisDetectionData getDetectionData()
        {
            ThemisDetectionData toReturn = new ThemisDetectionData();
            toReturn.statistics = GetInputsStatistics();
            toReturn.momentsOfInterest = momentsOfInterest;
            toReturn.remarks = remarks;
            return toReturn;
        }
        public string getDescription()
        {
            string toReturn = "";
            return toReturn;
        }

        public async Task<bool> Analyze(UserText text)
        {
            _text = text;
            GetObjectResponse inputsData = await _userTextRepository.S3GetInputDataAsync(text);
            IEnumerable<Input> inputs = ReadInputsStream(inputsData.ResponseStream, noHash:true);
            await AnalyzeInputs(inputs.ToList());
            return true;
        } 
        public ThemisDetector(IUserTextRepository userTextRepository, ApplicationUser user) {
            _user = user;
            _userTextRepository = userTextRepository;
        }
    }
}
