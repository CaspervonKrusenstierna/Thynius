using ThyniusWeb.Server.Models;

namespace ThyniusWeb.Server.Interfaces
{
    public interface IOrganizationRepository
    {
       public Task<IEnumerable<Organization>> GetAllAsync();
       public Task<Organization?> GetByEmailExtensionAsync(string EmailExtension);
       public bool Add(Organization organization);
       public bool Update(Organization organization);
       public bool Delete(Organization organization);
       public bool Save();
    }
}
