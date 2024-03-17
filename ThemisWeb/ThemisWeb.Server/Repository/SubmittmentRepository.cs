using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class SubmittmentRepository : ISubmittmentRepository
    {
        ApplicationDbContext _context;
        public SubmittmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Submittment>> GetAssignmentSubmittments(Assignment assignment) {
            return await _context.Submittments.Where(i => i.AssignmentId == assignment.Id).ToListAsync();
        }

        public async Task<IEnumerable<Submittment>> GetUserSubmittments(ApplicationUser user)
        {
            return await _context.Submittments.Where(i => i.OwnerId == user.Id).ToListAsync();
        }
        public bool Add(Submittment submittment)
        {
            _context.Add(submittment);
            return Save();
        }
        public bool Update(Submittment submittment)
        {
            _context.Update(submittment);
            return Save();
        }
        public bool Delete(Submittment submittment)
        {
            _context.Remove(submittment);
            return Save();
        }
        public async Task<Submittment> GetByIdAsync(int id)
        {
            return await _context.Submittments.FindAsync(id);
        }
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
