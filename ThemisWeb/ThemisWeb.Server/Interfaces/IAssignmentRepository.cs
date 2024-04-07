using Amazon.S3.Model;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Interfaces
{
    public interface IAssignmentRepository
    {
        public Task<IEnumerable<Assignment>> GetByGroupIdAsync(int groupId);
        public Task<IEnumerable<Assignment>> GetUserAssignmentsAsync(ApplicationUser user);
        public Task<Assignment> GetByIdAsync(int id);
        public Task<PutObjectResponse> UploadAssignmentPictureAsync(Assignment assignment, IFormFile assignmentPicture);
        public Task<DeleteObjectResponse> DeletAssignmentPictureAsync(Assignment assignment);
        public string GetSignedAssignmentImgUrl(Assignment assignment);
        public bool Add(Assignment assignment);
        public bool Update(Assignment assignment);
        public Task<bool> Delete(Assignment assignment);
        public bool Save();
    }
}
