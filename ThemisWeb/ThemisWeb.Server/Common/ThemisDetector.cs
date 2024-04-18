

using Amazon.S3.Model;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using static ThemisWeb.Server.Common.ThemistTextConverter;

namespace ThemisWeb.Server.Common
{
    public struct MomentOfInterest //This data is outputted to the teacher so the teacher can quickly navigate and look at the moment of interest. Example of this is a paste.
    {
        public string reason;
        public int Index;
    }
    public struct InputsStatistic // This statistic is outputted to the teacher. Example: "Editscore: 3".
    {
        public string infoType;
        public string info;
    }
    public class ThemisDetectionData //Data thats interesting to the teacher or analyzer in question
    {
        public List<String> remarks;
        public List<MomentOfInterest> momentsOfInterest;
        public List<InputsStatistic> statistics;
    }
    
    private struct InternalThemisDetectorInputsData{ //This is the data that the inputs analyzer outputs for further processing
            int totalAddCharTime;
            int addCharAmount;
            int deleteCharAmount;
            int deleteSectionAmount;
            int extendedTypingBreaksCount;
            int smallPasteCount;
            int totalExternalPasteChars;
            List<MomentOfInterest> momentsOfInterest;
    }
    private struct InternalThemisDetectorInputsProcessingData{ //Data used by the algo to process the inputs
        UInt64 currSessionRelativeTime;
        int previousAddCharRelativeTime;
        string currRawContent;
        IUserTextRepository userTextRepository;
        ApplicationUser user;
        UserText text;
    }
    private struct InternalThemisDetectorTextData{ //the final processed data.
        private float editScore;
        private float averageTypeSpeed;
        private int detectionScore;
        private List<String> remarks;
    }
    public class ThemisDetector
    {
        private InternalThemisDetectorInputsData inputsData = new InternalThemisDetectorInputsData();
        private InternalThemisDetectorInputsProcessingData inputsProcessingData  = new InternalThemisDetectorInputsProcessingData();
        private InternalThemisDetectorTextData textData = new InternalThemisDetectorTextData();
        
        private async Task<bool> wasTextTypedByHandBefore(string Text, UInt64 timePoint)
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

        private inline void onAdd(Input input){
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
        }
        
        private inline void onDelete(Input input){
             deleteCharAmount++; 
        }
        
        private inline void onPaste(Input input){
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
        }
        
        private inline void onSessionStart(Input input){
            currSessionRelativeTime = input.relativeTimePointMs;
        }

        private inline void processAverageTypeSpeed(){
            if(addCharAmount == 0)
            {
                averageTypeSpeed = totalAddCharTime;
            }
            else
            {
                averageTypeSpeed = totalAddCharTime/1000/addCharAmount;
            }
        }
        private inlinevoid processEditScore(){
            if(deleteCharAmount == 0)
            {
                editScore = 0;
            }
            else
            {
                editScore = addCharAmount / deleteCharAmount;
            }
        }
        private inline void processAnalyzerData(){
            processAverageTypeSpeed();
            processEditScore();
        }
        
        private async Task AnalyzeInputs(List<Input> inputs)
        {
            for (int i = 0; inputs.Count() > i; i++)
            {
                Input input = inputs[i];
                switch (input._ActionType)
                {
                    case ActionType.ADDCHAR: onAdd(input); break;

                    case ActionType.DELETECHAR: onDelete(input); break;

                    case ActionType.PASTE: onPaste(input); break;
                        
                    case ActionType.SESSIONSTART: onSessionStart(input); break;
                }
                AdvanceInput(ref RawContent, input);
            }
            processAnalyzerData();
            return;
        }

        private inline int getRoundedDetectionScore(){
            if(score >= 3)
            {
                return 3;
            }
            return (int)(score);
        }
        private inline void factorInPastes(){
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
        }
        private inline void factorInSmallPastes(){
            if(smallPasteCount >= 20)
            {
                remarks.Add("Användaren uppvisade besynnerligt beteende i form av många små inklistringar. Totalt antal små inklistringar: " + smallPasteCount);
                score += 1;
            }
        }
        private inline void factorInEditScore(){
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
        }
        
        public int GetDetectionScore()
        {
            factorInEditScore();
            factorInSmallPastes();
            factorInPastes();
            
            return getRoundedDetectionScore();
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
