using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Interfaces
{
    public interface IUserTextRepository
    {
        public Task<IEnumerable<UserText>> GetUserTexts(ApplicationUser user);
        public Task<UserText> GetByIdAsync(int id);

        // public Task<PutObjectResponse> S3RawContentUpload(UserText text, IFormFile rawContent);
        // public Task<DeleteObjectResponse> S3RawContentDelete(UserText text);
        // public Task<string> S3GetRawContentSignedUrlAsync(UserText text);

        // public Task<PutObjectResponse> S3InputDataUpload(UserText text, IFormFile inputData);
        // public Task<DeleteObjectResponse> S3InputDataDelete(UserText text);
        // public Task<string> S3GetInputDataSignedUrlAsync(UserText text);
        public Task<UserText> GetByTextData(ApplicationUser user, int CharCount, int WordCount);
        public bool Add(UserText text);
        public bool Update(UserText text);
        public bool Delete(UserText text);
        public bool Save();

    }
}
