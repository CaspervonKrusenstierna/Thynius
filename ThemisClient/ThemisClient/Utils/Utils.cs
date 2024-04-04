using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ThemisClient.Utils
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

        public ActionType ActionType;
        public UInt32 SelectionStart;
        public UInt32 SelectionEnd;
        public UInt64 RelativeTimeMs;
    }

    public static class Utils
    {
        public static string ConvertJsonData(string json)
        {
            Debug.WriteLine(json);
            IEnumerable<Input> unFiltered = Newtonsoft.Json.JsonConvert.DeserializeObject<Input[]>(json);
            Debug.WriteLine(unFiltered.Count());
            foreach(Input item in unFiltered)
            {
                Debug.WriteLine(item.ActionType);
                Debug.WriteLine(item.RelativeTimeMs);
                Debug.WriteLine("SELECTIONSTART:" + item.SelectionStart);
                Debug.WriteLine("SELECTIONEND:" + item.SelectionEnd);
                //filtered.Add(new Output { ActionType=item.ActionType, RelativeTimeMs=item.RelativeTimeMs, Selection=item.Selection, ActionContent=Encoding.Unicode.GetString(item.ActionContent)});
            }
            return "";
        }
    }
}
