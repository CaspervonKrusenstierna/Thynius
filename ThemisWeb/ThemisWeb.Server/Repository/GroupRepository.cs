using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configurationManager;
        private readonly IAmazonS3 _amazonS3;
        public GroupRepository(ApplicationDbContext context, IAmazonS3 amazonS3, IConfiguration configurationManager)
        {
            this._context = context;
            this._configurationManager = configurationManager;
            this._amazonS3 = amazonS3;
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

        public async Task<PutObjectResponse> UploadGroupPictureAsync(Group group, IFormFile groupPicture)
        {
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _configurationManager["BucketName"],
                Key = $"group_images/{group.Id}",
                ContentType = groupPicture.ContentType,
                InputStream = groupPicture.OpenReadStream()
            };
            return await _amazonS3.PutObjectAsync(putObjectRequest);
        }
        public async Task<DeleteObjectResponse> DeleteGroupPictureAsync(Group group)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _configurationManager["BucketName"],
                Key = $"group_images/{group.Id}"
            };
            return await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
        }
        public async Task<string> GetSignedGroupImgUrlAsync(Group group)
        {
            GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = _configurationManager["BucketName"],
                Key = $"group_images/{group.Id}",
                Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

        };
            return await _amazonS3.GetPreSignedURLAsync(getPreSignedUrlRequest);
        }
    }
}
