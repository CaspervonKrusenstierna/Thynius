﻿using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThyniusWeb.Server.Models;
using static ThyniusWeb.Server.Common.ThyniustTextConverter;

namespace ThyniusWeb.Server.Interfaces
{
    public interface IUserTextRepository
    {
        public Task<IEnumerable<UserText>> GetUserTexts(ApplicationUser user);
        public Task<UserText> GetByIdAsync(int id);
        public Task<PutObjectResponse> S3RawContentUpload(UserText text, string rawContent);
        public Task<DeleteObjectResponse> S3RawContentDelete(UserText text);
        public string S3GetRawContentSignedUrl(UserText text);
        public Task<PutObjectResponse> S3InputDataUpload(UserText text, Stream stream);
        public Task<DeleteObjectResponse> S3InputDataDelete(UserText text);
        public Task<GetObjectResponse> S3GetInputDataAsync(UserText text);
        public string S3GetInputDataSignedUrl(UserText text);
        public Task<PutObjectResponse> S3DetectionDataUpload(UserText text, string detectionData);
        public Task<DeleteObjectResponse> S3DetectionDataDelete(UserText text);
        public string S3GetDetectionDataSignedUrl(UserText text);
        public Task<IEnumerable<UserText>> GetAssignmentTexts(Assignment assignment);
        public Task<UserText> GetByTextData(ApplicationUser user, UInt64 guid);
        public bool Add(UserText text);
        public bool Update(UserText text);
        public bool Delete(UserText text);
        public bool Save();

    }
}
