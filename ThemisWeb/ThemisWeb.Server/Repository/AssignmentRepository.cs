using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IGroupRepository _groupRepository;
        public AssignmentRepository(ApplicationDbContext context, IGroupRepository groupRepository)
        {
            _context = context;
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<Assignment>> GetUserAssignmentsAsync(ApplicationUser user)
        {
            IEnumerable<Group> userGroups = await _groupRepository.GetUserGroups(user.Id);
            IEnumerable<int> userGroupIds = userGroups.Select(group => group.Id);
            return await _context.Assignments.Where(i => userGroupIds.Contains(i.GroupId)).ToListAsync();
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
