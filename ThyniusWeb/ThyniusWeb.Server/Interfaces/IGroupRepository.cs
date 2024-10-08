﻿using Amazon.S3.Model;
using ThyniusWeb.Server.Models;

namespace ThyniusWeb.Server.Interfaces
{
    public interface IGroupRepository
    {
        public bool Add(Group group);
        public bool Update(Group group);
        public Task<bool> Delete(Group group);
        public Task<IEnumerable<Group>> GetUserGroups(string userId);
        public Task<PutObjectResponse> UploadGroupPictureAsync(Group group, IFormFile groupPicture);
        public Task<DeleteObjectResponse> DeleteGroupPictureAsync(Group group);
        public string GetSignedGroupImgUrl(Group group);
        public Task<Group> GetAssignmentGroup(Assignment assignment);
        public Task<Group> GetByIdAsync(int id);
        public bool Save();
    }
}
