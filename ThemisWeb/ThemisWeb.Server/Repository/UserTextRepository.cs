using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class UserTextRepository : IUserTextRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public UserTextRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IEnumerable<UserText>> GetUserTexts(ApplicationUser user)
        {
            return await _context.UserTexts.Where(t => t.OwnerId == user.Id).ToListAsync();
        }

        public async Task<UserText> GetByIdAsync(int id)
        {
            return await _context.UserTexts.FindAsync(id);
        }

        /*public Task<PutObjectResponse> S3RawContentUpload(UserText text, IFormFile rawContent);
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
       public Task<DeleteObjectResponse> S3RawContentDelete(UserText text, IFormFile rawContent)
       {
           var deleteObjectRequest = new DeleteObjectRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"raw_textcontent/{text.Id}"
           };
           return await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
       }
       public Task<string> S3GetRawContentSignedUrlAsync(UserText text, IFormFile rawContent)
       {
           GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"raw_textcontent/{text.Id}",
               Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

       };
           return await _amazonS3.GetPreSignedURLAsync(getPreSignedUrlRequest);
       }*/

        public bool Add(UserText text)
        {
            _context.Add(text);
            return Save();
        }

        public bool Update(UserText text)
        {
            _context.Update(text);
            return Save();
        }
        public bool Delete(UserText text)
        {
            _context.Remove(text);
            return Save();
        }
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }

    }
}
