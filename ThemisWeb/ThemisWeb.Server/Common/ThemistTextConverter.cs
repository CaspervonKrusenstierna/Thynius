
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;
using FileHelpers;
namespace ThemisWeb.Server.Common
{
    public enum ActionType
    {
        ADDCHAR = 0,
        DELETESELECTION = 1,
        PASTE = 2,
        DELETECHAR = 3
    };

    public static class ThemistTextConverter
    {
        [DelimitedRecord(",")]
        public class Input
        {
            [FieldQuoted('"')]
            public string ActionContent;
            public ActionType ActionType;
            public UInt32 SelectionStart;
            public UInt32 SelectionEnd;
            public UInt64 RelativeTimeMs;
        }
        public static IEnumerable<Input> ReadInputs(IFormFile file)
        {
            var reader = new StreamReader(file.OpenReadStream());
            string fileContent = reader.ReadToEnd();
            Console.WriteLine(fileContent);
            var fileHelperEngine = new FileHelperEngine<Input>();
            var inputs = fileHelperEngine.ReadString(fileContent);
            reader.Dispose();

            return inputs;
        }


        public static string GetInputsRawText(IFormFile inputsFile)
        {
            string toReturn = "";

            IEnumerable<Input> inputs = ReadInputs(inputsFile);
            foreach (Input input in inputs)
            {
                switch (input.ActionType)
                {
                    case ActionType.ADDCHAR: 
                        toReturn = toReturn.Insert((int)input.SelectionStart, input.ActionContent); 
                        break;

                    case ActionType.DELETESELECTION: 
                        toReturn = toReturn.Remove((int)input.SelectionStart, (int)input.SelectionEnd - (int)input.SelectionStart);
                        break;

                    case ActionType.PASTE: 
                        if(input.SelectionStart != input.SelectionEnd) // REPLACE OPERATION
                        {
                            toReturn = toReturn.Remove((int)input.SelectionStart, (int)input.SelectionEnd - (int)input.SelectionStart);
                        }
                        toReturn = toReturn.Insert((int)input.SelectionStart, input.ActionContent); 
                        break;
                }
            }
            return toReturn;
        }
    }
}
