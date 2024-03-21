namespace ThemisWeb.Server.Data
{
    public class GroupData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GroupDataExtended
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public UserData ManagerData { get; set; }
        public IEnumerable<dynamic> userDatas { get; set; }
        public IEnumerable<dynamic> assignmentDatas { get; set; }
    }
}
