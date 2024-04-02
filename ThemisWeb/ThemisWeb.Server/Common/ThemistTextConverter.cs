namespace ThemisWeb.Server.Common
{
    public enum ActionType
    {
        ADDCHAR = 0,
        DELETESELECTION = 1,
        PASTE = 2,
        DELETECHAR = 3
    };
    public struct Selection
    {
        public UInt32 SelectionStart;
        public UInt32 SelectionEnd;
    };

    public struct Input
    {
        public ActionType _ActionType;
        public string ActionContent;
        public Selection _Selection;
        public UInt64 relativeTimePointMs;
    }
    public struct TextData
    {
        public string rawContent;
        public int wordCount;
        public int charCount;
    }
    public class ThemistTextConverter
    {
    }
}
