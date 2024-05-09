using Amazon.S3.Model;
using ThyniusWeb.Server.Models;

namespace ThyniusWeb.Server.Interfaces
{
    public interface IUserRepository
    {
        public bool AddUserToGroup(ApplicationUser user, Group Group);
        public bool RemoveUserFromGroup(ApplicationUser user, Group group);
        public Task<IEnumerable<ApplicationUser>> GetAllAsync();
        public Task<ApplicationUser> GetByIdAsync(string id);
        public Task<IEnumerable<ApplicationUser>> getOrganizationTeachers(string organization);
        public Task<IEnumerable<ApplicationUser>> GetOrganizationUsers(string organization);
        public Task<IEnumerable<ApplicationUser>> GetSearchUsers(string search, string organization, int max);
        public Task<IEnumerable<ApplicationUser>> GetGroupUsers(Group group);
        public Task<PutObjectResponse> UploadUserProfilePictureAsync(ApplicationUser user, MemoryStream stream);
        public Task<ApplicationUser> GetByEmailAsync(string email);
        public Task<DeleteObjectResponse> DeleteUserProfilePictureAsync(ApplicationUser user);
        public Task<string> GetSignedUserProfileImgUrlAsync(ApplicationUser user);
        public Task<int> GetUserRoleLevel(ApplicationUser user);
        public bool Add(ApplicationUser user);
        public bool Update(ApplicationUser user);
        public bool Delete(ApplicationUser user);
        public bool Save();
    }
}
    