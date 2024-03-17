using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Interfaces
{
    public interface IGroupRepository
    {
        public bool Add(Group classtoadd);
        public bool Update(Group classtoupdate);
        public bool Delete(Group classtodelete);

        public Task<Group> GetByIdAsync(int id);
        public bool Save();
    }
}
