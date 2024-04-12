using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IGroupRepository _groupRepository;
        private readonly IAmazonS3 _amazonS3;
        private readonly IConfiguration _configuration;
        private readonly IUserTextRepository _userTextRepository;
        public AssignmentRepository(ApplicationDbContext context, IGroupRepository groupRepository, IUserTextRepository userTextRepository, IAmazonS3 amazonS3, IConfiguration configuration)
        {
            _context = context;
            _groupRepository = groupRepository;
            _amazonS3 = amazonS3;
            _configuration = configuration; 
            _userTextRepository = userTextRepository;
        }

        public async Task<PutObjectResponse> UploadAssignmentPictureAsync(Assignment assignment, IFormFile assignmentPicture)
        {
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _configuration["BucketName"],
                Key = $"assignment_images/{assignment.Id}",
                ContentType = assignmentPicture.ContentType,
                InputStream = assignmentPicture.OpenReadStream()
            };
            return await _amazonS3.PutObjectAsync(putObjectRequest);
        }
        public async Task<DeleteObjectResponse> DeletAssignmentPictureAsync(Assignment assignment)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _configuration["BucketName"],
                Key = $"assignment_images/{assignment.Id}"
            };
            return await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
        }
        public string GetSignedAssignmentImgUrl(Assignment assignment)
        {
            GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = _configuration["BucketName"],
                Key = $"assignment_images/{assignment.Id}",
                Verb = HttpVerb.GET,
                Expires = DateTime.Now.AddHours(1)

            };
            return _amazonS3.GetPreSignedURL(getPreSignedUrlRequest);
        }
        public async Task<IEnumerable<Assignment>> GetUserAssignmentsAsync(ApplicationUser user)
        {
            IEnumerable<Group> userGroups = await _groupRepository.GetUserGroups(user.Id);
            IEnumerable<int> userGroupIds = userGroups.Select(group => group.Id);
            return await _context.Assignments.Where(i => userGroupIds.Contains(i.GroupId)).ToListAsync();
        }
        public async Task<IEnumerable<Assignment>> GetByGroupIdAsync(int groupId)
        {
            return await _context.Assignments.Where(i => i.GroupId == groupId).ToListAsync();
        }
        public async Task<Assignment> GetByIdAsync(int id)
        {
            return await _context.Assignments.FindAsync(id);
        }
        public bool Add(Assignment assignment)
        {
            _context.Add(assignment);
            return Save();
        }
        public bool Update(Assignment assignment)
        {
            _context.Update(assignment);
            return Save();
        }
        public async Task<bool> Delete(Assignment assignment)
        {
            IEnumerable<UserText> assignmentTexts = await _userTextRepository.GetAssignmentTexts(assignment);
            foreach (UserText text in assignmentTexts)
            {
                _userTextRepository.Delete(text);
            }

            await DeletAssignmentPictureAsync(assignment);
            _context.Assignments.Remove(assignment);
            return Save();
        }
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
