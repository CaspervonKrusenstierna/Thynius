using System;
using System.Collections.Generic;

namespace ThemisWeb.Server.Common
{
    public class DataClasses
    {
        public class GroupData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PictureLink { get; set; }
        }

        public class GroupDataExtended
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string DateCreated { get; set; }
            public dynamic ManagerData { get; set; }
            public IEnumerable<dynamic> userDatas { get; set; }
            public IEnumerable<dynamic> assignmentDatas { get; set; }
        }

        public class AssignmentData
        {
            public string Name;
            public int ID;
            public string DueDate;
        }
        public class UserData
        {
            public string Username;
            public string ID;
            public int RoleLevel;
            public string ProfilePictureUrl;
        }

        public enum ActionType
        {
            ADDCHAR = 0,
            DELETESELECTION = 1,
            PASTE = 2
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
        public struct ThemisSessionData
        {
            public IEnumerable<Input> inputs;
        }
    }
  }
