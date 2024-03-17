using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public GroupRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool Add(Group classtoadd)
        {
            _context.Add(classtoadd);
            return Save();
        }
        public bool Update(Group classtoupdate)
        {
            _context.Update(classtoupdate);
            return Save();
        }
        public bool Delete(Group classtodelete)
        {
            _context.Update(classtodelete);
            return Save();
        }
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await _context.Groups.FindAsync(id);
        }
    }
}
