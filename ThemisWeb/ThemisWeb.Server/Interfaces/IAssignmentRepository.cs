using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Interfaces
{
    public interface IAssignmentRepository
    {
        public Task<IEnumerable<Assignment>> GetByGroupIdAsync(int groupId);
        public Task<IEnumerable<Assignment>> GetUserAssignmentsAsync(ApplicationUser user);
        public Task<Assignment> GetByIdAsync(int id);
        public bool Add(Assignment assignment);
        public bool Update(Assignment assignment);
        public bool Delete(Assignment assignment);
        public bool Save();
    }
}
