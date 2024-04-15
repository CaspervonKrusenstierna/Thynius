using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using System.Text;
using System.Text.Unicode;
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

        public async Task<IEnumerable<UserText>> GetAssignmentTexts(Assignment assignment)
        {
            return await _context.UserTexts.Where(i => i.AssignmentId == assignment.Id).ToListAsync();
        }

        public async Task<PutObjectResponse> S3RawContentUpload(UserText text, string rawContent)
       {
            byte[] byteArray = Encoding.UTF8.GetBytes(rawContent);
            MemoryStream stream = new MemoryStream(byteArray);

            var putObjectRequest = new PutObjectRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_rawcontent/{text.Id}",
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
       public string S3GetRawContentSignedUrl(UserText text)
       {
           GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_rawcontent/{text.Id}",
               Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

       };
           return _amazonS3.GetPreSignedURL(getPreSignedUrlRequest);
       }

        public async Task<PutObjectResponse> S3InputDataUpload(UserText text, Stream stream)
        {
            var putObjectRequest = new PutObjectRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_inputdata/{text.Id}",
               ContentType = "text/plain",
               InputStream = stream
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
        public string S3GetInputDataSignedUrl(UserText text)
        {
           GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_inputdata/{text.Id}",
               Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

        };
           return _amazonS3.GetPreSignedURL(getPreSignedUrlRequest);
        }
        public async Task<GetObjectResponse> S3GetInputDataAsync(UserText text)
        {
            GetObjectRequest getObjectRequest = new GetObjectRequest
            {
                BucketName = _configurationManager["BucketName"],
                Key = $"text_inputdata/{text.Id}",
            };
            return await _amazonS3.GetObjectAsync(getObjectRequest);
        }
        public async Task<PutObjectResponse> S3DetectionDataUpload(UserText text, string detectionData){
            byte[] byteArray = Encoding.UTF8.GetBytes(detectionData);
            MemoryStream stream = new MemoryStream(byteArray);

            var putObjectRequest = new PutObjectRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_detectiondata/{text.Id}",
               ContentType = "application/json",
               InputStream = stream
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

        public string S3GetDetectionDataSignedUrl(UserText text){
           GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
           {
               BucketName = _configurationManager["BucketName"],
               Key = $"text_detectiondata/{text.Id}",
               Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

           };
           return _amazonS3.GetPreSignedURL(getPreSignedUrlRequest);
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
