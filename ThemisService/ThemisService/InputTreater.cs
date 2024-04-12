using FileHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ThemisService
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
        public uint SelectionStart { get; set; }
        public uint SelectionEnd { get; set; }
        public ulong Cp { get; set; }
        public ulong relativeTimePointMs { get; set; }
    }
    public struct Input
    {
        public string ActionContent;
        public ActionType _ActionType;
        public uint SelectionStart;
        public uint SelectionEnd;
        public ulong relativeTimePointMs;
    }
    public class InputTreater
    {
        Input[] treatedInputs;
        List<unTreatedInput> toTreat;
        InputsMetaData metaData;

        private Input unTreatedToTreated(unTreatedInput input)
        {
            return new Input()
            {
                ActionContent = input.ActionContent,
                relativeTimePointMs = input.relativeTimePointMs,
                _ActionType = input._ActionType,
                SelectionStart = input.SelectionStart,
                SelectionEnd = input.SelectionEnd
            };
        }
        private int GetPasteDiff(unTreatedInput previousInput, unTreatedInput currInput)
        {
            return Math.Abs((int)previousInput.Cp + previousInput.ActionContent.Length - (int)currInput.Cp);
        }
        private int GetDeleteDiff(unTreatedInput previousInput, unTreatedInput currInput)
        {
            return Math.Abs((int)previousInput.SelectionEnd - (int)previousInput.SelectionStart + (int)currInput.Cp - (int)previousInput.Cp);
        }
        private void TreatFirstInput()
        {
            treatedInputs[0] = unTreatedToTreated(toTreat[0]);
        }
        private void TreatInputsInMiddle()
        {

            for (int i = 1; toTreat.Count-1 > i; i++)
            {
                unTreatedInput currInput = toTreat[i];
                unTreatedInput prevInput = toTreat[i - 1];

                if (prevInput._ActionType == ActionType.PASTE)
                {
                    int diff = GetPasteDiff(prevInput, currInput);
                    Debug.WriteLine("PasteDiff: " + diff);
                    if (diff != 0)
                    {
                        if (diff == 2)
                        {
                            string result = " " + treatedInputs[i - 1].ActionContent + "\n";
                            treatedInputs[i - 1].ActionContent = result;
                        }
                        else
                        {
                            treatedInputs[i - 1].ActionContent += "\n";
                        }
                    }
                }
                if (prevInput._ActionType == ActionType.DELETESELECTION && !(prevInput.SelectionStart == prevInput.SelectionEnd))
                {
                    int diff = GetDeleteDiff(prevInput, currInput);
                    if (diff != 0)
                    {
                        treatedInputs[i - 1].SelectionEnd -= 1;
                    }
                }

                treatedInputs[i] = unTreatedToTreated(currInput);
            }
        }
        private void TreatLastInput()
        {
            unTreatedInput lastInput = toTreat[toTreat.Count() - 1];
            unTreatedInput inputBeforeLastInput = toTreat[toTreat.Count() - 2];

            if (inputBeforeLastInput._ActionType == ActionType.PASTE)
            {
                int diff = GetPasteDiff(inputBeforeLastInput, lastInput);
                if (diff != 0)
                {
                    if (diff == 2)
                    {
                        string result = " " + treatedInputs[treatedInputs.Length - 2].ActionContent + "\n";
                        treatedInputs[treatedInputs.Length - 2].ActionContent = result;
                    }
                    else
                    {
                        treatedInputs[treatedInputs.Length - 2].ActionContent += "\n";
                    }
                }
            }
            if(inputBeforeLastInput._ActionType == ActionType.DELETESELECTION && !(inputBeforeLastInput.SelectionStart == inputBeforeLastInput.SelectionEnd))
            {
                int diff = GetDeleteDiff(inputBeforeLastInput, lastInput);
                if (diff != 0)
                {
                    treatedInputs[treatedInputs.Length - 2].SelectionEnd -= 1;
                }
            }

            if (lastInput._ActionType == ActionType.PASTE)
            {
                int diff = Math.Abs((int)lastInput.Cp + lastInput.ActionContent.Length - metaData.endCp);
                if (diff != 0)
                {
                    lastInput.ActionContent = lastInput.ActionContent + "\n";
                    if (diff == 2)
                    {
                        string result = " " + lastInput.ActionContent + "\n";
                        lastInput.ActionContent = result;
                    }
                }
            }
            if (lastInput._ActionType == ActionType.DELETESELECTION && !(lastInput.SelectionStart == lastInput.SelectionEnd))
            {
                int diff = Math.Abs((int)lastInput.SelectionEnd - (int)lastInput.SelectionStart + metaData.endCp - (int)lastInput.Cp);
                Debug.WriteLine("DIFFDIFFIDIFF: " + diff);
                if (diff != 0)
                {
                    lastInput.SelectionEnd -= 1;
                }
            }

            treatedInputs[treatedInputs.Length - 1] = unTreatedToTreated(lastInput);
        }

        public InputTreater(List<unTreatedInput> toTreat, InputsMetaData metaData)
        {
            this.treatedInputs = new Input[toTreat.Count];
            this.toTreat = toTreat;
            this.metaData = metaData;
        }
        public Input[] getTreatedInputs()
        {
            TreatFirstInput();
            TreatInputsInMiddle();
            TreatLastInput();
            return this.treatedInputs;
        }

    }
}
