using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAmazonS3 _amazonS3;
        private readonly IConfiguration _configurationManager;
        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IAmazonS3 amazonS3, IConfiguration configurationManager)
        {
            _context = context;
            _userManager = userManager;
            _amazonS3 = amazonS3;
            _configurationManager = configurationManager;
        }

        public async Task<IEnumerable<ApplicationUser>> getOrganizationTeachers(string organization)
        {
            List<ApplicationUser> toReturn = new List<ApplicationUser>();
            var users = _context.Users.Include(u => u.Organization).Where(i => i.Organization.EmailExtension == organization);
            foreach(ApplicationUser user in users)
            {
                IEnumerable<String> roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Teacher"))
                {
                    toReturn.Add(user);
                }
            }
            return toReturn;
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
        public async Task<IEnumerable<ApplicationUser>> GetSearchUsers(string search, string organization, int max)
        {
            return await _context.Users.Where(i => i.OrganizationEmailExtension == organization && i.UserName.StartsWith(search)).Take(max).ToListAsync();
        }

        
        public async Task<PutObjectResponse> UploadUserProfilePictureAsync(ApplicationUser user, MemoryStream stream)
        {
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _configurationManager["BucketName"],
                Key = $"profile_images/{user.Id}",
                ContentType = "multipart/form-data",
                InputStream = stream
            };
            return await _amazonS3.PutObjectAsync(putObjectRequest);
        }
        public async Task<DeleteObjectResponse> DeleteUserProfilePictureAsync(ApplicationUser user)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _configurationManager["BucketName"],
                Key = $"profile_images/{user.Id}"
            };
            return await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
        }

        public async Task<string> GetSignedUserProfileImgUrlAsync(ApplicationUser user)
        {
            GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = _configurationManager["BucketName"],
                Key = $"profile_images/{user.Id}",
                Expires = DateTime.UtcNow.AddHours(1)
            };
            return await _amazonS3.GetPreSignedURLAsync(getPreSignedUrlRequest);
        }
        public async Task<int> GetUserRoleLevel(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            int highestRoleLevel = 0;
            foreach (var role in roles)
            {
                int currentRowLevel = 0;
                if (role == "Teacher")
                {
                    currentRowLevel = 1;
                }
                if (role == "OrganizationAdmin")
                {
                    currentRowLevel = 2;
                }
                if (role == "Admin")
                {
                    currentRowLevel = 3;
                }
                if (currentRowLevel > highestRoleLevel)
                {
                    highestRoleLevel = currentRowLevel;
                }

            }
            return highestRoleLevel;
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
