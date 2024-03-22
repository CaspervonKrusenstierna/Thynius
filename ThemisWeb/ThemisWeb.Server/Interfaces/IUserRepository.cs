using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Interfaces
{
    public interface IUserRepository
    {
        public bool AddUserToGroup(ApplicationUser user, Group Group);
        public bool RemoveUserFromGroup(ApplicationUser user, Group group);
        public Task<IEnumerable<ApplicationUser>> GetAllAsync();
        public Task<ApplicationUser> GetByIdAsync(string id);
        public Task<IEnumerable<ApplicationUser>> GetOrganizationUsers(string organization);
        public Task<IEnumerable<ApplicationUser>> GetSearchUsers(string search, string organization, int max);
        public Task<IEnumerable<ApplicationUser>> GetGroupUsers(Group group);
        public Task<int> GetUserRoleLevel(ApplicationUser user);
        public bool Add(ApplicationUser user);
        public bool Update(ApplicationUser user);
        public bool Delete(ApplicationUser user);
        public bool Save();
    }
}
    