using System;
using System.Collections.Generic;

namespace ThyniusWeb.Server.Common
{
    public class DataClasses
    {
        public class GroupData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ManagerId { get; set; }
            public string PictureLink { get; set; }
        }

        public class GroupDataExtended
        {
            public string Name { get; set; }
            public string DateCreated { get; set; }
            public dynamic ManagerData { get; set; }
            public IEnumerable<dynamic> assignmentDatas { get; set; }
        }
        public class AssignmentDataExtended
        {
            public string Description;
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

        public class UserTextData
        {
            public string OwnerName;
            public string OwnerID;
            public string AssignmentName;
            public string detectionDataUrl;
        }
    }
  }
