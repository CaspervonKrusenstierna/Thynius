using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Controllers
{
    [Authorize(Roles = "VerifiedUser")]
    [Route("/submittment")]
    public class SubmittmentController : Controller
    {
        IAssignmentRepository _assignmentRepository;
        IUserRepository _userRepository;
        UserManager<ApplicationUser> _userManager;
        ISubmittmentRepository _submittmentRepository;
        IGroupRepository _groupRepository;
        public SubmittmentController(ISubmittmentRepository submittmentRepository, IUserRepository userRepository, IGroupRepository groupRepository, IAssignmentRepository assignmentRepository, UserManager<ApplicationUser> userManager)
        {
           _submittmentRepository = submittmentRepository;
           _userRepository = userRepository;
           _userManager = userManager;
           _assignmentRepository = assignmentRepository;
           _groupRepository = groupRepository;

        }

        [HttpPost]
        public async Task<IActionResult> CreateSubmittment(int textId, int assignmentId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Assignment assignmentToAddTo = await _assignmentRepository.GetByIdAsync(assignmentId);
            if (assignmentToAddTo == null)
            {
                return BadRequest();
            }

            Group groupToAddTo = await _groupRepository.GetByIdAsync(assignmentToAddTo.GroupId);
            IEnumerable<ApplicationUser> GroupUsers = await _userRepository.GetGroupUsers(groupToAddTo);
            if (!GroupUsers.Contains(user))
            {
                return Unauthorized();
            }
            
            Submittment newSubmittment = new Submittment();
            newSubmittment.AssignmentId = assignmentId;
            newSubmittment.UserTextId = textId;

            if (!_submittmentRepository.Add(newSubmittment))
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubmittment(int submittmentId) // This function wont be open to users so no need to assert ownership like in others
        {
            Submittment toDelete = await _submittmentRepository.GetByIdAsync(submittmentId);
            if (toDelete == null)
            {
                return BadRequest();
            }

            bool result = _submittmentRepository.Delete(toDelete);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
