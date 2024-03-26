using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Interfaces
{
    public interface ITextSessionRepository
    {
        // public Task<string> S3GetSignedUrlAsync(TextSession session);

        // public Task<PutObjectResponse> S3Upload(TextSession session, IFormFile sessionData);

        // public Task<DeleteObjectResponse> S3Delete(TextSession session)

        public Task<IEnumerable<TextSession>> GetUserTextSessions(UserText text);
        public Task<TextSession> GetByIdAsync(int id);
        public bool Add(TextSession session);
        public bool Update(TextSession session);
        public bool Delete(TextSession session);
        public bool Save();
    }
}
