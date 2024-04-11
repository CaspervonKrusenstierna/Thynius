

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

        private IEnumerable<Input> pastes;
        public IEnumerable<Input> deleteSelections;
        private IEnumerable<Input> addChars;
        private IEnumerable<Input> deleteChars;
        private IEnumerable<Input> truePastes;

        private int deletesPerAdds;
        private int editScore;
        private int averageTypeSpeed;
        private int detectionScore;
        private List<MomentOfInterest> momentsOfInterest;
        private async Task<bool> wasTextTypedByHandBefore(string currRawContent, string Text, UInt64 timePoint)
        {
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
        private async Task<bool> AnalyzeInputs(IEnumerable<Input> inputs)
        {
            int previousAddCharRelativeTime = 0;
            int totalAddCharTime = 0;
            int addCharAmount = 0;
            int extendedTypingBreaksCount = 0;

            string currRawContent = "";
            UInt64 currSessionRelativeTime = 0;

            foreach (Input input in inputs)
            {
                switch (input._ActionType)
                {
                    case ActionType.ADDCHAR:
                        addChars.Append(input);
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

                    case ActionType.DELETECHAR: deleteChars.Append(input); break;

                    case ActionType.DELETESELECTION: deleteSelections.Append(input); break;

                    case ActionType.PASTE:
                        bool wasTypedBefore = await wasTextTypedByHandBefore(currRawContent, input.ActionContent, currSessionRelativeTime+input.relativeTimePointMs);
                        if (!wasTypedBefore)
                        {
                            truePastes.Append(input);
                        }
                        break;
                case ActionType.SESSIONSTART:
                    currSessionRelativeTime = input.relativeTimePointMs;
                    break;
                }
                AdvanceInput(ref currRawContent, input);
            }
            averageTypeSpeed = totalAddCharTime / addCharAmount;
            deletesPerAdds = addCharAmount / deleteChars.Count();
            return true;
        }
        public int GetDetectionScore()
        {
            return detectionScore;
        }
        public ThemisDetectionData getDetectionData()
        {
            ThemisDetectionData toReturn = new ThemisDetectionData();
            toReturn.statistics.Add(new InputsStatistic { infoType = "Genomsnitts skrivhastighet", info = averageTypeSpeed.ToString() });
            toReturn.statistics.Add(new InputsStatistic { infoType = "Ändrings poäng", info = editScore.ToString() });
            toReturn.momentsOfInterest = momentsOfInterest;
            return toReturn;
        }
        public string getDescription()
        {
            string toReturn = "";
            return toReturn;
        }

        public async void Analyze(UserText text)
        {
            _text = text;
            GetObjectResponse inputsData = await _userTextRepository.S3GetInputDataAsync(text);
            await AnalyzeInputs(ReadInputsStream(inputsData.ResponseStream));
        } 
        public ThemisDetector(IUserTextRepository userTextRepository, ApplicationUser user) {
            _user = user;
            _userTextRepository = userTextRepository;
        }
    }
}
