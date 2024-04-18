
using Amazon.S3.Model;
using FileHelpers;
using static ThemisWeb.Server.Common.ThemistTextConverter;
namespace ThemisWeb.Server.Common
{
    public enum ActionType
    {
        ADDCHAR = 0,
        DELETESELECTION = 1,
        PASTE = 2,
        DELETECHAR = 3,
        SPELLINGREPLACE = 7,
        SESSIONSTART = 8
    };

    public static class ThemistTextConverter
    {
        [DelimitedRecord(",")]
        public class Input
        {
            [FieldQuoted()]
            public string ActionContent;
            public ActionType _ActionType;
            public int SelectionStart;
            public int SelectionEnd;
            public ulong relativeTimePointMs;
        }
        public static IEnumerable<Input> ReadInputsStream(Stream stream, bool noHash=false)
        {
            if (!noHash)
            {
                stream.Position = 0;
            }
            var reader = new StreamReader(stream, leaveOpen: true);
            string fileContent = reader.ReadToEnd();
            var fileHelperEngine = new FileHelperEngine<Input>();
            var inputs = fileHelperEngine.ReadString(fileContent);
            reader.Dispose();
            return inputs;
        }
        public static IEnumerable<Input> ReadInputs(IFormFile file)
        {
            return ReadInputsStream(file.OpenReadStream());
        }
        public static IEnumerable<Input> ReadInputsResponse(GetObjectResponse response) {
            return ReadInputsStream(response.ResponseStream);
        }
        public static void AdvanceInput(ref string currRawText, Input input)
        {
            input.ActionContent = input.ActionContent.Replace("\r\n", "\n");
            switch (input._ActionType)
            {
                case ActionType.ADDCHAR:
                    currRawText = currRawText.Insert(input.SelectionStart, input.ActionContent);
                    break;

                case ActionType.DELETESELECTION:
                    if (input.SelectionStart == input.SelectionEnd)
                    {
                        currRawText = currRawText.Remove(input.SelectionStart - 1, 1);
                    }
                    else
                    {
                        currRawText = currRawText.Remove(input.SelectionStart, input.SelectionEnd - input.SelectionStart);
                    }
                    break;

                case ActionType.PASTE:
                    if (input.SelectionStart != input.SelectionEnd) // REPLACE OPERATION
                    {
                        currRawText = currRawText.Remove(input.SelectionStart, input.SelectionEnd - input.SelectionStart);
                    }
                    currRawText = currRawText.Insert(input.SelectionStart, input.ActionContent);
                    break;

                case ActionType.SPELLINGREPLACE:
                    currRawText = currRawText.Remove(input.SelectionStart, input.SelectionEnd-input.SelectionStart);
                    currRawText = currRawText.Insert(input.SelectionStart, input.ActionContent);
                    break;
            }
        }
        public static string GetInputsRawText(IEnumerable<Input> inputs)
        {
            string toReturn = "";

            foreach (Input input in inputs)
            {
                AdvanceInput(ref toReturn, input);
            }
            return toReturn;
        }

        public static string GetInputsRawTextAtTimePoint(List<Input> inputs, UInt64 timePoint)
        {
            if(inputs.Count == 0)
            {
                return "";
            }

            string toReturn = "";
            UInt64 currTime = inputs[0].relativeTimePointMs;

            for(int i = 1; inputs.Count > i; i++)
            {
                if (inputs[i]._ActionType == ActionType.SESSIONSTART)
                {
                    currTime = inputs[i].relativeTimePointMs;
                    continue;
                }
                AdvanceInput(ref toReturn, inputs[i]);
                if (currTime >= timePoint )
                {
                    if((currTime - timePoint) >= 21600000) // 6 hours
                    {
                        return "";
                    }
                    else
                    {
                        return toReturn;
                    }
                }
            }
            return toReturn;
        }
        public static string GetTextTitle(string text)
        {
            if(text.Length < 14)
            {
                return text;
            }
            return text.Substring(0, 13);
        }
    }
}
