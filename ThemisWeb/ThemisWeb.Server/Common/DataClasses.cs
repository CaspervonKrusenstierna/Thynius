namespace ThemisWeb.Server.Common
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
}
