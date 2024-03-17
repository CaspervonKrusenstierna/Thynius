using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Interfaces
{
    public interface IGroupRepository
    {
        public bool Add(Group group);
        public bool Update(Group group);
        public bool Delete(Group group);
        public Task<IEnumerable<Group>> GetUserGroups(string userId);
        public Task<Group> GetByIdAsync(int id);
        public bool Save();
    }
}
