

using Amazon.S3.Model;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;
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
        public List<MomentOfInterest> momentsOfInterest;
        public List<InputsStatistic> statistics;
    }
    public class ThemisDetector
    {
        IUserTextRepository _userTextRepository;
        ApplicationUser _user;
        UserText _text;

        private int editScore;
        private int averageTypeSpeed;
        private int detectionScore = 0;
        private List<MomentOfInterest> momentsOfInterest = new List<MomentOfInterest>();
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

            string currRawContent = "";
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
                        bool wasTypedBefore = await wasTextTypedByHandBefore(currRawContent, input.ActionContent, currSessionRelativeTime+input.relativeTimePointMs);
                        if (!wasTypedBefore)
                        {
                            momentsOfInterest.Add(new MomentOfInterest{ Index=i, reason="PASTE"});
                        }
                        break;
                case ActionType.SESSIONSTART:
                    currSessionRelativeTime = input.relativeTimePointMs;
                    break;
                }
                AdvanceInput(ref currRawContent, input);
            }
            if(addCharAmount == 0)
            {
                averageTypeSpeed = totalAddCharTime;
            }
            else
            {
                averageTypeSpeed = totalAddCharTime / addCharAmount;
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
            return detectionScore;
        }
        public ThemisDetectionData getDetectionData()
        {
            ThemisDetectionData toReturn = new ThemisDetectionData();
            List<InputsStatistic> temp = new List<InputsStatistic>();
            temp.Add(new InputsStatistic { infoType = "Genomsnitts skrivhastighet", info = averageTypeSpeed.ToString() });
            temp.Add(new InputsStatistic { infoType = "Ändrings poäng", info = editScore.ToString() });
            toReturn.statistics = temp;
            toReturn.momentsOfInterest = momentsOfInterest;
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
