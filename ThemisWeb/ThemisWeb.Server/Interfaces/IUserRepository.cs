using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Interfaces
{
    public interface IUserRepository
    {
        public bool AddUserToGroup(ApplicationUser user, int GroupId);
        public Task<IEnumerable<ApplicationUser>> GetAllAsync();
        public Task<ApplicationUser> GetByIdAsync(string id);
        public Task<IEnumerable<ApplicationUser>> GetOrganizationUsers(string organization);
        public bool Add(ApplicationUser user);
        public bool Update(ApplicationUser user);
        public bool Delete(ApplicationUser user);
        public bool Save();
    }
}
    