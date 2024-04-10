

using System.Text.Json;
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
        private bool wasTextTypedByHandBefore(string currRawContent, string Text)
        {
            return currRawContent.Contains(Text);
        }
        private void AnalyzeInputs(IEnumerable<Input> inputs)
        {
            int previousAddCharRelativeTime = 0;
            int totalAddCharTime = 0;
            int addCharAmount = 0;
            int extendedTypingBreaksCount = 0;

            string currRawContent = "";

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
                        if (!wasTextTypedByHandBefore(currRawContent, input.ActionContent))
                        {
                            truePastes.Append(input);
                        }
                        break;
                }
                AdvanceInput(ref currRawContent, input);
            }
            averageTypeSpeed = totalAddCharTime / addCharAmount;
            deletesPerAdds = addCharAmount / deleteChars.Count();
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

        public ThemisDetector(Stream inputData) {
            AnalyzeInputs(ReadInputsStream(inputData));
        }
    }
}
