
using System.Collections.Generic;
using System.IO;

namespace ThemisWeb.Server.Common
{
    public enum ActionType
    {
        ADDCHAR = 0,
        DELETESELECTION = 1,
        PASTE = 2,
        DELETECHAR = 3
    };
    public struct Input
    {
        public string ActionContent;
        public ActionType ActionType;
        public UInt32 SelectionStart;
        public UInt32 SelectionEnd;
        public UInt64 RelativeTimeMs;
    }
    public static class ThemistTextConverter
    {
        private static IEnumerable<Input> ReadInputs(IFormFile file)
        {
            var reader = new StreamReader(file.OpenReadStream());
            string fileContent = reader.ReadToEnd();
            Console.WriteLine(fileContent);
            reader.Dispose();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Input[]>(fileContent);
        }


        public static string GetInputsRawText(IFormFile inputsFile)
        {
            string toReturn = "";

            IEnumerable<Input> inputs = ReadInputs(inputsFile);
            foreach (Input input in inputs)
            {
                Console.WriteLine("STATE: " + toReturn);
                Console.WriteLine("TYPE: " + input.ActionType);
                Console.WriteLine("CONTENT: " + input.ActionContent);
                Console.WriteLine("START: " + input.SelectionStart);
                Console.WriteLine("END: " + input.SelectionEnd);
                switch (input.ActionType)
                {
                    case ActionType.ADDCHAR: toReturn = toReturn.Insert((int)input.SelectionStart, input.ActionContent); break;
                    case ActionType.DELETESELECTION: toReturn = toReturn.Remove((int)input.SelectionStart, (int)input.SelectionEnd - (int)input.SelectionStart); break;
                    case ActionType.PASTE: toReturn = toReturn.Insert((int)input.SelectionStart, input.ActionContent); break;
                }
            }
            return toReturn;
        }
    }
}
