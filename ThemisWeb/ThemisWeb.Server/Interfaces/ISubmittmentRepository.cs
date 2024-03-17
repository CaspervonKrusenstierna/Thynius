using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Interfaces
{
    public interface ISubmittmentRepository
    {
        public bool Add(Submittment classtoadd);
        public bool Update(Submittment classtoupdate);
        public bool Delete(Submittment classtodelete);
        public Task<IEnumerable<Submittment>> GetAssignmentSubmittments(Assignment assignment);
        public  Task<IEnumerable<Submittment>> GetUserSubmittments(ApplicationUser user);
        public Task<Submittment> GetByIdAsync(int id);
        public bool Save();
    }
}
