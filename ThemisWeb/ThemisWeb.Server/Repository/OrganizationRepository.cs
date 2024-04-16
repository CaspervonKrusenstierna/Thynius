using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        ApplicationDbContext _context;
        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            return await _context.Organizations.ToListAsync();
        }
        public async Task<Organization?> GetByEmailExtensionAsync(string EmailExtension)
        {
            return await _context.Organizations.FirstOrDefaultAsync(i => i.EmailExtension == EmailExtension);
        }
        
        public bool Add(Organization organization)
        {
            _context.Add(organization);
            return Save();
        }
        public bool Update(Organization organization)
        {
            _context.Update(organization);
            return Save();
        }
        public bool Delete(Organization organization)
        {
            _context.Remove(organization);
            return Save();
        }
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
