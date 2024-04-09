
using Amazon.S3.Model;
using FileHelpers;
namespace ThemisWeb.Server.Common
{
    public enum ActionType
    {
        ADDCHAR = 0,
        DELETESELECTION = 1,
        PASTE = 2,
        DELETECHAR = 3,
        SPELLINGREPLACE = 7
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
        public static IEnumerable<Input> ReadInputsStream(Stream stream)
        {
            stream.Position = 0;
            var reader = new StreamReader(stream, leaveOpen: true);
            string fileContent = reader.ReadToEnd();
            Console.WriteLine(fileContent);
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

        public static string GetInputsRawText(IEnumerable<Input> inputs)
        {
            string toReturn = "";

            foreach (Input input in inputs)
            {
                Console.WriteLine("Content: " + input.ActionContent);
                Console.WriteLine("Selstart: " + input.SelectionStart);
                Console.WriteLine("Selend: " + input.SelectionEnd);
                switch (input._ActionType)
                {
                    case ActionType.ADDCHAR: 
                        toReturn = toReturn.Insert(input.SelectionStart, input.ActionContent); 
                        break;

                    case ActionType.DELETESELECTION:
                        if (input.SelectionStart == input.SelectionEnd)
                        {
                            toReturn = toReturn.Remove(input.SelectionStart-1, 1);
                        }
                        else
                        {
                            toReturn = toReturn.Remove(input.SelectionStart, input.SelectionEnd - input.SelectionStart);
                        }
                        break;

                    case ActionType.PASTE: 
                        if(input.SelectionStart != input.SelectionEnd) // REPLACE OPERATION
                        {
                            toReturn = toReturn.Remove(input.SelectionStart, input.SelectionEnd - input.SelectionStart);
                        }
                        toReturn = toReturn.Insert(input.SelectionStart, input.ActionContent); 
                        break;
                    case ActionType.SPELLINGREPLACE:
                        toReturn = toReturn.Remove(input.SelectionStart, input.SelectionEnd - input.SelectionStart);
                        toReturn = toReturn.Insert(input.SelectionStart, input.ActionContent);
                        break;
                }
                Console.WriteLine("CurrToReturn: " + toReturn);
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
