using Amazon.S3.Model;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Interfaces
{
    public interface IGroupRepository
    {
        public bool Add(Group group);
        public bool Update(Group group);
        public bool Delete(Group group);
        public Task<IEnumerable<Group>> GetUserGroups(string userId);
        public Task<PutObjectResponse> UploadGroupPictureAsync(Group group, IFormFile groupPicture);
        public Task<DeleteObjectResponse> DeleteGroupPictureAsync(Group group);
        public Task<string> GetSignedGroupImgUrlAsync(Group group);
        public Task<Group> GetByIdAsync(int id);
        public bool Save();
    }
}
