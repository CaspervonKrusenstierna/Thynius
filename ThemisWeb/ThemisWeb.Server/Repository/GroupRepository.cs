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
        public bool Add(Group group)
        {
            _context.Add(group);
            return Save();
        }
        public bool Update(Group group)
        {
            _context.Update(group);
            return Save();
        }
        public bool Delete(Group group)
        {
            _context.Update(group);
            return Save();
        }
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }

        public async Task<IEnumerable<Group>> GetUserGroups(string userId)
        {
            return _context.Groups.Where(i => (i.Users.First(c => (c.Id == userId)) != null || i.ManagerId == userId)).ToList();
        }
        public async Task<Group> GetByIdAsync(int id)
        {
            return await _context.Groups.FindAsync(id);
        }
    }
}
