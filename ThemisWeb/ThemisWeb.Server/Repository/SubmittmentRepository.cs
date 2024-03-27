using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class SubmittmentRepository : ISubmittmentRepository
    {
        ApplicationDbContext _context;
        IUserTextRepository _userTextRepository;
        public SubmittmentRepository(ApplicationDbContext context, IUserTextRepository userTextRepository)
        {
            _context = context;
            _userTextRepository = userTextRepository;
        }

        public async Task<IEnumerable<Submittment>> GetAssignmentSubmittments(Assignment assignment) {
            return await _context.Submittments.Where(i => i.AssignmentId == assignment.Id).ToListAsync();
        }

        public async Task<IEnumerable<Submittment>> GetUserSubmittments(ApplicationUser user)
        {
            IEnumerable<int> userTextIds = await _context.UserTexts.Where(i => i.OwnerId == user.Id).Select(i => i.Id).ToListAsync();
            return await _context.Submittments.Where(i => userTextIds.Contains(i.UserTextId)).ToListAsync();
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
