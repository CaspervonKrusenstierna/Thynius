using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {

        private readonly ApplicationDbContext _context;
        public AssignmentRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Assignment>> GetByGroupIdAsync(int groupId)
        {
            return await _context.Assignments.Where(i => i.GroupId == groupId).ToListAsync();
        }
        public async Task<Assignment> GetByIdAsync(int id)
        {
            return await _context.Assignments.FindAsync(id);
        }
        public bool Add(Assignment assignment)
        {
            _context.Add(assignment);
            return Save();
        }
        public bool Update(Assignment assignment)
        {
            _context.Update(assignment);
            return Save();
        }
        public bool Delete(Assignment assignment)
        {
            _context.Update(assignment);
            return Save();
        }
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
