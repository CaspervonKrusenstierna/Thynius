

using Amazon.S3.Model;
using System.Xml.Linq;
using ThyniusWeb.Server.Interfaces;
using ThyniusWeb.Server.Models;
using static ThyniusWeb.Server.Common.ThyniustTextConverter;

namespace ThyniusWeb.Server.Common
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
    public class ThyniusDetectionData //Data thats interesting to the teacher or analyzer in question
    {
        public List<String> remarks;
        public List<MomentOfInterest> momentsOfInterest;
        public List<InputsStatistic> statistics;
    }
    
    struct InternalThyniusDetectorInputsData{ //This is the data that the inputs analyzer outputs for further processing
            public int totalAddCharTime;
            public int addCharAmount;
            public int deleteCharAmount;
            public int deleteSectionAmount;
            public int extendedTypingBreaksCount;
            public int smallPasteCount;
            public int totalExternalPasteChars;
            public List<MomentOfInterest> momentsOfInterest;
    }
    struct InternalThyniusDetectorInputsProcessingData{ //Data used by the algo to process the inputs
        public UInt64 currSessionRelativeTime;
        public int previousAddCharRelativeTime;
        public IUserTextRepository userTextRepository;
        public string currRawContent;
        public ApplicationUser user;
        public UserText text;
    }
    struct InternalThyniusDetectorTextData{ //the final processed data.
        public float editScore;
        public float averageTypeSpeed;
        public float detectionScore;
        public List<String> remarks;
    }
    public class ThyniusDetector
    {
        private InternalThyniusDetectorInputsData inputsData = new InternalThyniusDetectorInputsData();
        private InternalThyniusDetectorInputsProcessingData inputsProcessingData  = new InternalThyniusDetectorInputsProcessingData();
        private InternalThyniusDetectorTextData textData = new InternalThyniusDetectorTextData();
        
        private async Task<bool> wasTextTypedByHandBefore(string Text, UInt64 timePoint)
        {
            return inputsProcessingData.currRawContent.Contains(Text);
            if (!inputsProcessingData.currRawContent.Contains(Text))
            {
                IEnumerable<UserText> userTexts = await inputsProcessingData.userTextRepository.GetUserTexts(inputsProcessingData.user);
                bool wasTypedInOtherDoc = false;
                foreach(UserText text in userTexts) //TODO: MAKE A CHECK ON CLIENT SIDE IF MULTIPLE DOCS WERE OPEN TO FILTER OUT MOST OF THESE CHECKS AS THEY VERY HEAVY
                {
                    if(text.Id == inputsProcessingData.text.Id)
                    {
                        continue;
                    }
                    GetObjectResponse currTextObjResponse = await inputsProcessingData.userTextRepository.S3GetInputDataAsync(text);
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

        private void onAdd(Input input){
                int timeDiff = (int)input.relativeTimePointMs - inputsProcessingData.previousAddCharRelativeTime;
                if (timeDiff >= 60000)
                {
                    inputsData.extendedTypingBreaksCount++;
                }
                else
                {
                    inputsData.addCharAmount++;
                    inputsData.totalAddCharTime += timeDiff;
                }
        }
        
        private void onDelete(Input input){
             inputsData.deleteCharAmount++; 
        }
        
        private async void onPaste(Input input, int index){
            if(input.ActionContent.Length <= 10) // probably just pasting in some external one word or char that user could not type on their keyboard
            {
                inputsData.smallPasteCount++;
            }
            bool wasTypedBefore = await wasTextTypedByHandBefore(input.ActionContent, inputsProcessingData.currSessionRelativeTime+input.relativeTimePointMs);
            if (!wasTypedBefore)
            {
                inputsData.totalExternalPasteChars += input.ActionContent.Length;
                inputsData.momentsOfInterest.Add(new MomentOfInterest{ Index=index, reason="PASTE"});
            }
        }
        
        private void onSessionStart(Input input){
            inputsProcessingData.currSessionRelativeTime = input.relativeTimePointMs;
        }

        private void processAverageTypeSpeed(){
            if(inputsData.addCharAmount == 0)
            {
                textData.averageTypeSpeed = inputsData.totalAddCharTime;
            }
            else
            {
                textData.averageTypeSpeed = inputsData.totalAddCharTime /1000/ inputsData.addCharAmount;
            }
        }
        private void processEditScore(){
            if(inputsData.deleteCharAmount == 0)
            {
                textData.editScore = 0;
            }
            else
            {
                textData.editScore = inputsData.addCharAmount / inputsData.deleteCharAmount;
            }
        }
        private void processAnalyzerData(){
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

                    case ActionType.PASTE: onPaste(input, i); break;
                        
                    case ActionType.SESSIONSTART: onSessionStart(input); break;
                }
                AdvanceInput(ref inputsProcessingData.currRawContent, input);
            }
            processAnalyzerData();
            return;
        }

        private int getRoundedDetectionScore(float score){
            if(score >= 3)
            {
                return 3;
            }
            return (int)(score);
        }
        private void factorInPastes(){
            if(inputsData.totalExternalPasteChars >= inputsProcessingData.currRawContent.Length / 4)
            {
                textData.remarks.Add("Användaren har kopierat en stor del av sin text från externa källor.");
                textData.detectionScore += 2;
            }
            else if(inputsData.totalExternalPasteChars >= inputsProcessingData.currRawContent.Length / 10)
            {
                textData.detectionScore += 1;
                textData.remarks.Add("Användaren har kopierat delar av sin text från externa källor." + inputsData.smallPasteCount);
            }
        }
        private void factorInSmallPastes(){
            if(inputsData.smallPasteCount >= 20)
            {
                textData.remarks.Add("Användaren uppvisade besynnerligt beteende i form av många små inklistringar. Totalt antal små inklistringar: " + inputsData.smallPasteCount);
                textData.detectionScore += 1;
            }
        }
        private void factorInEditScore(){
            if(textData.editScore <= 0.2)
            {
                textData.remarks.Add("Användaren reviderade sin text ovanligt lite.");
                textData.detectionScore += 1;
            }
            else if(textData.editScore <= 0.5)
            {
                textData.detectionScore += (float)0.5 - textData.editScore;
            }
        }
        
        public int GetDetectionScore()
        {
            factorInEditScore();
            factorInSmallPastes();
            factorInPastes();
            
            return getRoundedDetectionScore(textData.detectionScore);
        }
        private List<InputsStatistic> GetInputsStatistics()
        {
            List<InputsStatistic> toReturn = new List<InputsStatistic>();
            toReturn.Add(new InputsStatistic { infoType = "Genomsnittlig skrivhastighet", info = textData.averageTypeSpeed.ToString() });
            toReturn.Add(new InputsStatistic { infoType = "Ändrings poäng", info = textData.editScore.ToString() });
            return toReturn;
        }
        
        public ThyniusDetectionData getDetectionData()
        {
            ThyniusDetectionData toReturn = new ThyniusDetectionData();
            toReturn.statistics = GetInputsStatistics();
            toReturn.momentsOfInterest = inputsData.momentsOfInterest;
            toReturn.remarks = textData.remarks;
            return toReturn;
        }

        public async Task<bool> Analyze(UserText text)
        {
            inputsProcessingData.text = text;
            GetObjectResponse inputsData = await inputsProcessingData.userTextRepository.S3GetInputDataAsync(text);
            IEnumerable<Input> inputs = ReadInputsStream(inputsData.ResponseStream, noHash:true);
            await AnalyzeInputs(inputs.ToList());
            return true;
        } 
        public ThyniusDetector(IUserTextRepository userTextRepository, ApplicationUser user) {
            inputsProcessingData.user = user;
            inputsProcessingData.userTextRepository = userTextRepository;
        }
    }
}
