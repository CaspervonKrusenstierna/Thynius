using FileHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThemisClient.Utils;


namespace ThemisClient.InputTreater
{
    public enum ActionType
    {
        ADDCHAR = 0,
        DELETESELECTION = 1,
        PASTE = 2,
        UNDO = 3,
        REDO = 4,
        END = 5
    };
    [DelimitedRecord(",")]
    public class InputsMetaData
    {
        public int endCp;
    }
    [DelimitedRecord(",")]
    public class unTreatedInput
    {
        [FieldQuoted('"')]
        public string ActionContent { get; set; }
        public ActionType _ActionType { get; set; }
        public UInt32 SelectionStart { get; set; }
        public UInt32 SelectionEnd { get; set; }
        public ulong Cp { get; set; }
        public ulong relativeTimePointMs { get; set; }
    }
    public struct Input
    {
        public string ActionContent;
        public ActionType _ActionType;
        public UInt32 SelectionStart;
        public UInt32 SelectionEnd;
        public ulong relativeTimePointMs;
    }
    public static class InputTreater
    {
        public static Input[] TreatInputs(List<unTreatedInput> toTreat, InputsMetaData metaData)
        {
            bool previousWasPaste = false;
            Input[] toReturn = new Input[toTreat.Count];
            for (int i = 0; toTreat.Count > i; i++)
            {
                unTreatedInput currInput = toTreat[i];
                if (previousWasPaste)
                {
                    unTreatedInput previousInput = toTreat[i - 1];
                    int diff = Math.Abs((int)previousInput.Cp + previousInput.ActionContent.Length - (int)currInput.Cp);
                    Debug.WriteLine("Diff: " + diff);
                    if (diff != 0)
                    {
                        if (diff == 2)
                        {
                            string result = " " + toReturn[i - 1].ActionContent + "\n";
                            toReturn[i - 1].ActionContent = result;
                        }
                        else
                        {
                            toReturn[i - 1].ActionContent = toReturn[i - 1].ActionContent + "\n";
                        }
                    }
                }

                if (currInput._ActionType == ActionType.PASTE)
                {
                    previousWasPaste = true;
                }
                else
                {
                    previousWasPaste = false;
                }
                Input toPush = new Input()
                {
                    ActionContent = currInput.ActionContent,
                    relativeTimePointMs = currInput.relativeTimePointMs,
                    _ActionType = currInput._ActionType,
                    SelectionStart = currInput.SelectionStart,
                    SelectionEnd = currInput.SelectionEnd
                };
                toReturn[i] = toPush;
            }
            unTreatedInput lastInput = toTreat[toTreat.Count - 1];
            if (lastInput._ActionType == ActionType.PASTE)
            {
                int diff = Math.Abs(metaData.endCp - ((int)lastInput.Cp + lastInput.ActionContent.Length));
                Debug.WriteLine("Diff: " + diff);
                if (diff != 0)
                {
                    toReturn[toReturn.Length - 1].ActionContent = toReturn[toReturn.Length - 1].ActionContent + "\n";
                    if (diff == 2)
                    {
                        string result = " " + toReturn[toReturn.Length - 1].ActionContent + "\n";
                        toReturn[toReturn.Length - 1].ActionContent = result;
                    }
                }
            }
            foreach(Input input in toReturn)
            {
                Debug.WriteLine("Content:" + input.ActionContent);
            }
            return toReturn;
        }

        public static void WriteInputsCsvString(Input[] inputs, string path)
        {
            string toReturn = "";
            foreach(Input input in inputs)
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
