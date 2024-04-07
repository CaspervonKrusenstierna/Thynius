
using FileHelpers;
using System.Diagnostics;

namespace ThemisClient.Utils
{
    public enum ActionType
    {
        ADDCHAR = 0,
        DELETESELECTION = 1,
        PASTE = 2,
        DELETECHAR = 3
    };
    public static class Utils
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
        public static IEnumerable<Input> ReadInputs(string raw)
        {

            var fileHelperEngine = new FileHelperEngine<Input>();
            var inputs = fileHelperEngine.ReadString(raw);
            foreach (var input in inputs)
            {
                Console.WriteLine("CONTENT: " + input.ActionContent);
                Console.WriteLine("TYPE: " + input.ActionType);
                Console.WriteLine("SELECTSTART: " + input.SelectionStart);
                Console.WriteLine("SELECTEND: " + input.SelectionEnd);
                Console.WriteLine("RELATIVETIME: " + input.RelativeTimeMs);
            }
            return inputs;
        }

        private static void InsertStringInDict(string toInsert, Dictionary<UInt32, char> dict)
        {

        }
        private static void RemoveStringInDict(Dictionary<UInt32, char> dict) { }
        public static string ConvertDllData(string Data)
        {
            Debug.WriteLine(Data);
            Dictionary<UInt32, char> keyValuePairs = new Dictionary<UInt32, char>();
            IEnumerable<Input> inputs = ReadInputs(Data);

            foreach (var input in inputs)
            {
                switch (input.ActionType)
                {
                    case ActionType.ADDCHAR:
                        keyValuePairs.Add(input.SelectionStart, input.ActionContent[0]);
                        break;
                    case ActionType.DELETESELECTION:
                        for(UInt32 i = 0; input.SelectionEnd - input.SelectionStart > i; i++)
                        {
                            keyValuePairs.Remove(input.SelectionStart + i);
                        }
                        break;
                    case ActionType.PASTE:
                        for (UInt32 i = 0; input.SelectionEnd - input.SelectionStart > i; i++)
                        {
                            keyValuePairs.Add(input.SelectionStart + i, input.ActionContent[(int)(input.SelectionStart + i)]);
                        }
                        break;
                }
            }
            char[] chars = new char[keyValuePairs.Count];
            keyValuePairs.Values.CopyTo(chars, 0);
            Debug.WriteLine(new String(chars));
            return "";

        }
    }
}
