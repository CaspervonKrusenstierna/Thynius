

using static ThemisWeb.Server.Common.ThemistTextConverter;

namespace ThemisWeb.Server.Common
{
    public enum MomentOfInterestReason
    {
        PASTE = 1,
        REFERENCE = 2,
        EDIT = 3,
    }
    public struct MomentOfInterest
    {
        MomentOfInterestReason reason;
        public int startIndex;
        public int endIndex;
    }
    public struct ThemisDetectionData
    {
        public List<MomentOfInterest> momentsOfInterest;
        public int averageTypingSpeed;
        public int EditScore;
    }
    public class ThemisDetector
    {
        ThemisDetectionData detectionData;

        private IEnumerable<Input> _inputs;

        private IEnumerable<Input> pastes;
        public IEnumerable<Input> deleteSelections;
        private IEnumerable<Input> addChars;
        private IEnumerable<Input> deleteChars;

        private IEnumerable<Input> truePastes;
        int deletesPerAdds;
        int averageTypeSpeed;
        private bool wasTextTypedByHandBefore(string Text)
        {
            return false;
        }

        public int GetDetectionScore()
        {
            return 0;
        }

        public ThemisDetector(IEnumerable<Input> inputs) { 

            _inputs = inputs;

            int previousAddCharRelativeTime = 0;
            int totalAddCharTime = 0;
            int addCharAmount = 0;

            int extendedTypingBreaksCount = 0;

            foreach (Input input in inputs)
            {
                switch (input.ActionType)
                {
                    case ActionType.ADDCHAR:
                        addChars.Append(input);
                        int timeDiff = (int)input.RelativeTimeMs - previousAddCharRelativeTime;
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
                        if (!wasTextTypedByHandBefore(input.ActionContent))
                        {
                            truePastes.Append(input);
                        }
                        break;
                }
            }
            averageTypeSpeed = totalAddCharTime / addCharAmount;
            deletesPerAdds = addCharAmount / deleteChars.Count();

        }
    }
}
