using FileHelpers;

namespace ThemisService
{
    public static class Utils
    {
        public static void AdvanceInput(ref string currRawText, Input input)
        {
            Console.WriteLine("Content: " + input.ActionContent);
            Console.WriteLine("Selstart: " + input.SelectionStart);
            Console.WriteLine("Selend: " + input.SelectionEnd);
            switch (input._ActionType)
            {
                case ActionType.ADDCHAR:
                    currRawText = currRawText.Insert((int)input.SelectionStart, input.ActionContent);
                    break;

                case ActionType.DELETESELECTION:
                    if (input.SelectionStart == input.SelectionEnd)
                    {
                        currRawText = currRawText.Remove((int)input.SelectionStart - 1, 1);
                    }
                    else
                    {
                        currRawText = currRawText.Remove((int)input.SelectionStart, (int)input.SelectionEnd - (int)input.SelectionStart);
                    }
                    break;

                case ActionType.PASTE:
                    if (input.SelectionStart != input.SelectionEnd) // REPLACE OPERATION
                    {
                        currRawText = currRawText.Remove((int)input.SelectionStart, (int)input.SelectionEnd - (int)input.SelectionStart);
                    }
                    currRawText = currRawText.Insert((int)input.SelectionStart, input.ActionContent);
                    break;
            }
        }
        public static string GetInputsRawText(IEnumerable<Input> inputs)
        {
            string toReturn = "";

            foreach (Input input in inputs)
            {
                AdvanceInput(ref toReturn, input);
                Console.WriteLine("CurrToReturn: " + toReturn);
            }
            return toReturn;
        }
        public static void WriteInputsCsvString(Input[] inputs, string path)
        {
            string toReturn = "";
            foreach (Input input in inputs)
            {
                toReturn += '"' + input.ActionContent + '"' + ",";
                toReturn += (int)input._ActionType + ",";
                toReturn += input.SelectionStart + ",";
                toReturn += input.SelectionEnd + ",";
                toReturn += input.relativeTimePointMs + "\n";
            }
            File.WriteAllText(path + "_result", toReturn);
        }
        public static List<unTreatedInput> ReadInputs(string data)
        {
            var fileHelperEngine = new FileHelperEngine<unTreatedInput>();
            return fileHelperEngine.ReadString(data).ToList();
        }
        public static InputsMetaData readMetaData(string data)
        {
            var fileHelperEngine = new FileHelperEngine<InputsMetaData>();
            return fileHelperEngine.ReadString(data).ToList()[0];
        }
    }
}
