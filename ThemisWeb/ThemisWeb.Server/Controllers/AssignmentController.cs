using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Controllers
{
    [Authorize(Roles = "verifieduser")]
    
    public class AssignmentController : Controller
    {
        IAssignmentRepository _assignmentRepository;
        IGroupRepository _groupRepository;
        UserManager<ApplicationUser> _userManager;
        public AssignmentController(IAssignmentRepository assignmentRepository, IGroupRepository groupRepository, UserManager<ApplicationUser> userManager)
        {
            _assignmentRepository = assignmentRepository;
            _groupRepository = groupRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment(int groupId, string AssignmentName, string dueDate)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Group group = await _groupRepository.GetByIdAsync(groupId);

            if(group == null)
            {
                return BadRequest();
            }
            if(group.ManagerId != user.Id)
            {
                return Unauthorized();
            }


            Assignment newAssignment = new Assignment();
            newAssignment.Name = AssignmentName; // **TODO** Check if sql injection is possible
            newAssignment.GroupId = groupId;
            

            if (!_assignmentRepository.Add(newAssignment))
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAssignment(int assignmentId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Assignment toDelete = await _assignmentRepository.GetByIdAsync(assignmentId);
            Group toDeleteGroup = await _groupRepository.GetByIdAsync(toDelete.GroupId);

            if (toDelete == null)
            {
                return BadRequest();
            }
            if(toDeleteGroup.ManagerId != user.Id)
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
    }
}
