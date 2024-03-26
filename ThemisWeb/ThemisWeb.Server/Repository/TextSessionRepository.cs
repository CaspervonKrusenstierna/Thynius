using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using ReactApp1.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace ThemisWeb.Server.Repository
{
    public class TextSessionRepository : ITextSessionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public TextSessionRepository(ApplicationDbContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        /*
        public async Task<string> S3GetSignedUrlAsync(TextSession session)
        {
            GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = _configuration["BucketName"],
                Key = $"sessions/{session.Id}",
                Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

            };
            return await _amazonS3.GetPreSignedURLAsync(getPreSignedUrlRequest);
        }*/

        /*
        public async Task<DeleteObjectResponse> S3Delete(TextSession session)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _configuration["BucketName"],
                Key = $"profile_images/{session.Id}"
            };
            return await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
        }
         
         */

        /*
public async Task<PutObjectResponse> S3Upload(TextSession session, IFormFile sessionData)
{
    var putObjectRequest = new PutObjectRequest
    {
        BucketName = _configurationManager["BucketName"],
        Key = $"sessions/{session.Id}",
        ContentType = session.ContentType,
        InputStream = session.OpenReadStream()
    };
    return await _amazonS3.PutObjectAsync(putObjectRequest);
}*/

        public async Task<IEnumerable<TextSession>> GetUserTextSessions(UserText text)
        {
            return await _context.TextSessions.Where(i => i.TextId == text.Id).ToListAsync();
        }
        public async Task<TextSession> GetByIdAsync(int id)
        {
            return await _context.TextSessions.FindAsync(id);
        }
        public bool Add(TextSession session)
        {
            _context.Add(session);
            return Save();
        }
        public bool Update(TextSession session)
        {
            _context.Update(session);
            return Save();
        }
        public bool Delete(TextSession session)
        {
            //S3Delete(session);
            _context.Remove(session);
            return Save();
        }
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }

    }
}
