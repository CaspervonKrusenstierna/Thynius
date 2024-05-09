using FileHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ThyniusService
{
    public enum ActionType
    {
        SESSIONSTART = 0,
        END = 1,
        INVALID = 2,

        ADDCHAR = 3,
        DELETESELECTION = 4,
        PASTE = 5,
        SPELLINGREPLACE = 6,

        INSERTIMAGE = 7,
        INSERTBREAK = 8,
        INSERTFOOTNOTE = 9,
        ADDPAGE = 10,
        REMOVEPAGE = 11,

        ADDCHARFOOTNOTEWORLD = 12,
        DELETESELECTIONFOOTNOTEWORLD = 13,
        PASTEFOOTNOTEWORLD = 14,
        SPELLINGREPLACEFOOTNOTEWORLD = 15,

        ADDCHARHEADERWORLD = 16,
        DELETESELECTIONHEADERWORLD = 17,
        PASTEHEADERWORLD = 18,
        SPELLINGREPLACEHEADERWORLD = 19,
    };
    public enum World
    {
        NONE = 0,
        MainWorld = 1,
        HeaderWorld = 2,
        FootnoteWorld = 3,
    }

    [DelimitedRecord(",")]
    public class InputsMetaData
    {
        public ulong endMainCp;
        public ulong endHeaderCp;
        public ulong endFootnoteCp;
    }

    [DelimitedRecord(",")]
    public class unTreatedInput
    {
        [FieldQuoted('\uf0e1')]
        public string ActionContent { get; set; }
        public ActionType _ActionType { get; set; }
        public uint SelectionStart { get; set; }
        public uint SelectionEnd { get; set; }
        public ulong MainCp { get; set; }
        public ulong HeaderCp { get; set; }
        public ulong FootnoteCp { get; set; }
        public ulong relativeTimePointMs { get; set; }
    }

    [DelimitedRecord(",")]
    public class unTreatedItem
    {
        public ActionType _ItemType { get; set; }
        public uint SelectionStart { get; set; }
    }
    public struct Input
    {
        public string ActionContent;
        public ActionType _ActionType;
        public uint SelectionStart;
        public uint SelectionEnd;
        public World world;
        public ulong relativeTimePointMs;
    }

    public struct FilteredInput
    {
        public string ActionContent;
        public ActionType _ActionType;
        public uint SelectionStart;
        public uint SelectionEnd;
        public ulong relativeTimePointMs;
    }

    /* DO ABSOLUTELY EVERYTHING WE CAN HERE TO PUT STRESS OFF THE SERVER. OFC ANYTHING THAT COULD BE USED TO CHEAT SHOULDNT BE PUT HERE THOUGH. */
    public class Treater
    {
        Input[] treatedInputs;
        Input cachedPreviousTreated;
        List<unTreatedInput> inputsToTreat;
        List<Input> undoCache = new List<Input>();
        List<int> undoCacheIndices = new List<int>();
        InputsMetaData metaData;

        List<unTreatedItem> itemsToTreat;
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
        private int GetPasteDiff(unTreatedInput previousInput, unTreatedInput currInput, World world)
        {
            switch (world)
            {
                case World.MainWorld: return Math.Abs((int)previousInput.MainCp + previousInput.ActionContent.Length - (int)currInput.MainCp); break;
                case World.HeaderWorld: return Math.Abs((int)previousInput.HeaderCp + previousInput.ActionContent.Length - (int)currInput.HeaderCp); break;
                case World.FootnoteWorld: return Math.Abs((int)previousInput.FootnoteCp + previousInput.ActionContent.Length - (int)currInput.FootnoteCp); break;
            }
            return -1;
        }
        private int GetDeleteDiff(unTreatedInput previousInput, unTreatedInput currInput, World world)
        {
            switch(world)
            {
                case World.MainWorld: return Math.Abs((int)previousInput.SelectionEnd - (int)previousInput.SelectionStart + (int)currInput.MainCp - (int)previousInput.MainCp); break;
                case World.HeaderWorld: return Math.Abs((int)previousInput.SelectionEnd - (int)previousInput.SelectionStart + (int)currInput.HeaderCp - (int)previousInput.HeaderCp); break;
                case World.FootnoteWorld: return Math.Abs((int)previousInput.SelectionEnd - (int)previousInput.SelectionStart + (int)currInput.FootnoteCp - (int)previousInput.FootnoteCp); break;
            }
            return -1;
        }
        private World GetPrevInputWorld(unTreatedInput previousInput, unTreatedInput currInput)
        {
            if(previousInput.MainCp != currInput.MainCp)
            {
                return World.MainWorld;
            }
            if (previousInput.HeaderCp != currInput.HeaderCp && !(previousInput.HeaderCp < 24))
            {
                return World.HeaderWorld;
            }
            if (previousInput.FootnoteCp != currInput.FootnoteCp && !(previousInput.FootnoteCp < 1))
            {
                return World.FootnoteWorld;
            }
            return World.NONE;
        }

        private ActionType GetHeaderWorldActionTypeEquivalent(ActionType actionType)
        {
            ActionType toReturn;
            switch (actionType)
            {
                case ActionType.ADDCHAR: toReturn = ActionType.ADDCHARHEADERWORLD; break;
                case ActionType.PASTE: toReturn = ActionType.PASTEHEADERWORLD; break;
                case ActionType.DELETESELECTION: toReturn = ActionType.DELETESELECTIONHEADERWORLD; break;
                case ActionType.SPELLINGREPLACE: toReturn = ActionType.SPELLINGREPLACEHEADERWORLD; break;
                default: toReturn = actionType; break;
            }
            return toReturn;
        }

        private ActionType GetFootnoteWorldActionTypeEquivalent(ActionType actionType)
        {
            ActionType toReturn;
            switch (actionType)
            {
                case ActionType.ADDCHAR: toReturn =  ActionType.ADDCHARFOOTNOTEWORLD; break;
                case ActionType.PASTE: toReturn = ActionType.PASTEFOOTNOTEWORLD; break;
                case ActionType.DELETESELECTION: toReturn = ActionType.DELETESELECTIONFOOTNOTEWORLD; break;
                case ActionType.SPELLINGREPLACE: toReturn = ActionType.SPELLINGREPLACEFOOTNOTEWORLD; break;
                default: toReturn = actionType; break;
            }
            return toReturn;
        }
        private FilteredInput ConvertInputActionTypeWorld(Input input)
        {
            FilteredInput toReturn = new FilteredInput();
            toReturn.SelectionStart = input.SelectionStart;
            toReturn.SelectionEnd = input.SelectionEnd;
            toReturn.relativeTimePointMs = input.relativeTimePointMs;
            toReturn.ActionContent = input.ActionContent;
            switch (input.world)
            {
                case World.HeaderWorld: toReturn._ActionType = GetHeaderWorldActionTypeEquivalent(input._ActionType); break;
                case World.FootnoteWorld: toReturn._ActionType = GetFootnoteWorldActionTypeEquivalent(input._ActionType); break;
                default: toReturn._ActionType = input._ActionType; break;
            }
            return toReturn;
        }

        private void TreatInput(ref int i, unTreatedInput prevInput, unTreatedInput currInput)
        {
            World prevInputWorld = GetPrevInputWorld(prevInput, currInput);

            /*When you paste something in word, depending on where you paste, it might apply a space automatically 
                and also a newline. The following checks check for discrepencies and add chars if found.*/
            if (prevInput._ActionType == ActionType.PASTE)
            {
                int diff = GetPasteDiff(prevInput, currInput, prevInputWorld);

                if (diff != 0)
                {
                    if (diff == 2)
                    {
                        string result = " " + cachedPreviousTreated.ActionContent + "\n";
                        cachedPreviousTreated.ActionContent = result;
                    }
                    else
                    {
                        cachedPreviousTreated.ActionContent += "\n";
                    }
                }
            }

            if (prevInput._ActionType == ActionType.DELETESELECTION && !(prevInput.SelectionStart == prevInput.SelectionEnd))
            {
                int diff = GetDeleteDiff(prevInput, currInput, prevInputWorld);
                if (diff != 0)
                {
                    cachedPreviousTreated.SelectionEnd -= 1;
                }
            }

            treatedInputs[i - 1] = cachedPreviousTreated;
            treatedInputs[i - 1].world = prevInputWorld;
            cachedPreviousTreated = unTreatedToTreated(currInput);
        }

        private void TreatFirstInput()
        {
            cachedPreviousTreated = unTreatedToTreated(inputsToTreat[0]);
            treatedInputs[0] = unTreatedToTreated(inputsToTreat[0]);
        }
        private void TreatInputsInMiddle()
        {

            for (int i = 1; inputsToTreat.Count-1 > i; i++)
            {
                unTreatedInput currInput = inputsToTreat[i];
                unTreatedInput prevInput = inputsToTreat[i - 1];
                TreatInput(ref i, prevInput, currInput);
            }
        }
        private void TreatLastInput()
        {
            int lastInputIndex = inputsToTreat.Count() - 1;
            int inputBeforeLastInputIndex = lastInputIndex - 1;
            int IndexAfterLastIndex = lastInputIndex + 1;

            unTreatedInput endCpConvertedToInput = new unTreatedInput();
            endCpConvertedToInput.MainCp = metaData.endMainCp;
            endCpConvertedToInput.HeaderCp = metaData.endHeaderCp;
            endCpConvertedToInput.FootnoteCp = metaData.endFootnoteCp;

            unTreatedInput lastInput = inputsToTreat[lastInputIndex];
            unTreatedInput inputBeforeLastInput = inputsToTreat[inputBeforeLastInputIndex];

            TreatInput(ref lastInputIndex, inputBeforeLastInput, lastInput);
            TreatInput(ref IndexAfterLastIndex, lastInput, endCpConvertedToInput);
        }

        List<FilteredInput> getFilteredTreatedInputs()
        {
            List<FilteredInput> toReturn = new List<FilteredInput>();

            foreach(Input input in treatedInputs)
            {
                if(!(input.relativeTimePointMs == 0)) // if 0 we need to filter them out coz theyre invalid
                {
                    toReturn.Add(ConvertInputActionTypeWorld(input));
                }
            }
            return toReturn;
        }
        public Treater(List<unTreatedInput> inputsToTreat, InputsMetaData metaData)
        {
            this.inputsToTreat = inputsToTreat;
            this.metaData = metaData;
        }
        public List<FilteredInput> getTreatedInputs()
        {
            treatedInputs = new Input[inputsToTreat.Count()+5112];
            TreatFirstInput();
            TreatInputsInMiddle();
            TreatLastInput();
            return getFilteredTreatedInputs();
        }

    }
}
