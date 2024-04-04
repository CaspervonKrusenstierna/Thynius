using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using static ThemisWeb.Server.Common.DataClasses;

namespace ThemisWeb.Server.Controllers
{
    [Route("/assignment")]
    public class AssignmentController : Controller
    {
        IAssignmentRepository _assignmentRepository;
        IGroupRepository _groupRepository;
        UserManager<ApplicationUser> _userManager;
        IUserRepository _userRepository;
        public AssignmentController(IAssignmentRepository assignmentRepository, IUserRepository userRepository, IGroupRepository groupRepository, UserManager<ApplicationUser> userManager)
        {
            _assignmentRepository = assignmentRepository;
            _groupRepository = groupRepository;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<string> GetAssignmentData(int assignmentId)
        {
            Assignment assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
            if (assignment == null) {
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

            AssignmentDataExtended toReturn = new AssignmentDataExtended();
            return JsonSerializer.Serialize(new { assignment.Description, assignment.Name});
        }
        [HttpPost]
        public async Task<IActionResult> CreateAssignment(int groupId, string AssignmentName, string dueDate)
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            Group group = await _groupRepository.GetByIdAsync(groupId);

            if(group == null)
            {
                return BadRequest();
            }
            if(group.ManagerId != callingUser.Id)
            {
                return Unauthorized();
            }


            Assignment newAssignment = new Assignment();
            newAssignment.Name = AssignmentName;  //**TODO** Check if sql injection is possible
            newAssignment.GroupId = groupId;
            newAssignment.DueDate = dueDate;
            

            if (!_assignmentRepository.Add(newAssignment))
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAssignment(int assignmentId)
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            Assignment toDelete = await _assignmentRepository.GetByIdAsync(assignmentId);
            Group toDeleteGroup = await _groupRepository.GetByIdAsync(toDelete.GroupId);

            if (toDelete == null)
            {
                return BadRequest();
            }
            if(toDeleteGroup.ManagerId != callingUser.Id)
            {
                return Unauthorized();
            }

            bool result = _assignmentRepository.Delete(toDelete);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }


        [HttpGet]
        [Authorize(Roles = "VerifiedUser")]
        [Route("getuserassignments")]
        public async Task<string> getUserAssignments()
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            IEnumerable<Assignment> userAssignments = await _assignmentRepository.GetUserAssignmentsAsync(callingUser);
            return JsonSerializer.Serialize(userAssignments.Select(i => new {  i.Id,  i.Name, i.DueDate }));
        }
    }
}
