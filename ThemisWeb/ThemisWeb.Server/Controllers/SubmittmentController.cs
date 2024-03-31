using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using ThemisWeb.Server.Repository;
using static ThemisWeb.Server.Common.DataClasses;

namespace ThemisWeb.Server.Controllers
{
    [Route("/submittment")]
    public class SubmittmentController : Controller
    {
        IAssignmentRepository _assignmentRepository;
        IUserRepository _userRepository;
        UserManager<ApplicationUser> _userManager;
        ISubmittmentRepository _submittmentRepository;
        IGroupRepository _groupRepository;
        IUserTextRepository _userTextRepository;
        public SubmittmentController(ISubmittmentRepository submittmentRepository, IUserTextRepository userTextRepository, IUserRepository userRepository, IGroupRepository groupRepository, IAssignmentRepository assignmentRepository, UserManager<ApplicationUser> userManager)
        {
           _submittmentRepository = submittmentRepository;
           _userRepository = userRepository;
           _userManager = userManager;
           _assignmentRepository = assignmentRepository;
           _groupRepository = groupRepository;
            _userTextRepository = userTextRepository;
        }

        [HttpGet]
        [Route("getassignmentsubmittments")]
        public async Task<string> GetAssignmentSubmittments(int assignmentId)
        {
            Assignment assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            IEnumerable<Assignment> userAssignments = await _assignmentRepository.GetUserAssignmentsAsync(callingUser);
            Group assignmentGroup = await _groupRepository.GetAssignmentGroup(assignment);

            if (!userAssignments.Contains(assignment) && !(assignmentGroup.ManagerId == callingUser.Id))
            {
                HttpContext.Response.StatusCode = 401;
                return null;
            }

            IEnumerable<Submittment> assignmentSubmittments = await _submittmentRepository.GetAssignmentSubmittments(assignment);
            List<dynamic> assignmentDatas = [];
            foreach(var submittment in assignmentSubmittments)
            {
                var user = await _userRepository.GetSubmittmentUser(submittment);
                assignmentDatas.Add(new { submittment.Id, user.UserName, submittment.WarningLevel });
            }
            return JsonSerializer.Serialize(assignmentDatas);
        }

        [HttpGet]
        [Authorize(Roles = "VerifiedUser")]
        [Route("detectiondata")]
        public async void GetSubmittmentDetectionData(int submittmentId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize(Roles = "VerifiedUser")]
        public async Task<IActionResult> SubmitUserText(int textId, int assignmentId)
        {
            UserText text = await _userTextRepository.GetByIdAsync(textId);
            if (text == null)
            {
                return BadRequest();
            }
            Assignment assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return BadRequest();
            }
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            IEnumerable<ApplicationUser> assignmentUsers = await _userRepository.GetGroupUsers(await _groupRepository.GetByIdAsync(assignment.GroupId));
            if (text.OwnerId != callingUser.Id || !assignmentUsers.Contains(callingUser))
            {
                /*return Unauthorized();*/
            }

            Submittment submittment = new Submittment();
            submittment.UserTextId = textId;
            submittment.AssignmentId = assignmentId;
            submittment.TimeSubmitted = DateTime.Now.ToString();
            _submittmentRepository.Add(submittment);

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
