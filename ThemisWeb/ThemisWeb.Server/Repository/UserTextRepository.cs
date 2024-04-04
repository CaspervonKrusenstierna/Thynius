using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ReactApp1.Server.Data;
using System.Text;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class UserTextRepository : IUserTextRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configurationManager;
        private readonly IAmazonS3 _amazonS3;
        public UserTextRepository(ApplicationDbContext context, IAmazonS3 amazonS3, IConfiguration configuration)
        {
            _context = context;
            _configurationManager = configuration;
            _amazonS3 = amazonS3;
        }
        public async Task<IEnumerable<UserText>> GetUserTexts(ApplicationUser user)
        {
            return await _context.UserTexts.Where(t => t.OwnerId == user.Id).ToListAsync();
        }

        public async Task<UserText> GetByIdAsync(int id)
        {
            return await _context.UserTexts.FindAsync(id);
        }
        public async Task<UserText> GetByTextData(ApplicationUser user, UInt64 guid)
        {
            return await _context.UserTexts.Where(i => (i.OwnerId == user.Id && i.guid == guid)).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<UserText>> GetAssignmentTextsPage(Assignment assignment, int pageIndex, int pageSize)
        {
            return await _context.UserTexts.Where(i => i.AssignmentId == assignment.Id).Skip((pageIndex-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<PutObjectResponse> S3RawContentUpload(UserText text, string rawContent)
       {
            byte[] byteArray = Encoding.Unicode.GetBytes(rawContent);
            MemoryStream stream = new MemoryStream(byteArray);
            var putObjectRequest = new PutObjectRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_rawcontent/{text.Id}",
               ContentType = "multipart/form-data",
               InputStream = stream
           };
           return await _amazonS3.PutObjectAsync(putObjectRequest);
       }
       public async Task<DeleteObjectResponse> S3RawContentDelete(UserText text)
       {
           var deleteObjectRequest = new DeleteObjectRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_rawcontent/{text.Id}"
           };
           return await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
       }
       public async Task<string> S3GetRawContentSignedUrlAsync(UserText text)
       {
           GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_rawcontent/{text.Id}",
               Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

       };
           return await _amazonS3.GetPreSignedURLAsync(getPreSignedUrlRequest);
       }

        public async Task<PutObjectResponse> S3InputDataUpload(UserText text, IFormFile inputData)
        {
           var putObjectRequest = new PutObjectRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_inputdata/{text.Id}",
               ContentType = inputData.ContentType,
               InputStream = inputData.OpenReadStream()
           };
           return await _amazonS3.PutObjectAsync(putObjectRequest);
        }
        public async Task<DeleteObjectResponse> S3InputDataDelete(UserText text)
        {
           var deleteObjectRequest = new DeleteObjectRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_inputdata/{text.Id}"
           };
           return await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
        }
        public async Task<string> S3GetInputDataSignedUrlAsync(UserText text)
        {
           GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_inputdata/{text.Id}",
               Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

        };
           return await _amazonS3.GetPreSignedURLAsync(getPreSignedUrlRequest);
        }

        public async Task<PutObjectResponse> S3DetectionDataUpload(UserText text, IFormFile detectionData){
           var putObjectRequest = new PutObjectRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_detectiondata/{text.Id}",
               ContentType = detectionData.ContentType,
               InputStream = detectionData.OpenReadStream()
           };
           return await _amazonS3.PutObjectAsync(putObjectRequest);
        }

        public async Task<DeleteObjectResponse> S3DetectionDataDelete(UserText text){
           var deleteObjectRequest = new DeleteObjectRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_detectiondata/{text.Id}"
           };
           return await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
        }

        public async Task<string> S3GetDetectionDataSignedUrlAsync(UserText text){
           GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_detectiondata/{text.Id}",
               Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

           };
           return await _amazonS3.GetPreSignedURLAsync(getPreSignedUrlRequest);
        }
        
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
            S3RawContentDelete(text);
            S3InputDataDelete(text);
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
