using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using System.Reflection.Metadata.Ecma335;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ApplicationUser>> GetOrganizationUsers(string organization)
        {
            return _context.Users.Include(u => u.Organization).Where(i => i.Organization.EmailExtension == organization);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool AddUserToGroup(ApplicationUser user, Group group)
        {
            user.Groups.Add(_context.Groups.FirstOrDefault(c => c == group));
            return Update(user);
        }

        public bool RemoveUserFromGroup(ApplicationUser user, Group group)
        {
            user.Groups.Remove(_context.Groups.FirstOrDefault(c => c == group));
            return Update(user);
        }          
        public async Task<IEnumerable<ApplicationUser>> GetGroupUsers(Group group)
        {
            return await _context.Users.Include(i => i.Groups).Where(i => i.Groups.Contains(group)).ToListAsync();
        }
        public bool Add(ApplicationUser user)
        {
            _context.Add(user);
            return Save();
        }
        public bool Update(ApplicationUser user)
        {
            _context.Update(user);
            return Save();
        }
        public bool Delete(ApplicationUser user)
        {
            _context.Remove(user);
            return Save();
        }
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }

    }
}
