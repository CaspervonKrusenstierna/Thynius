using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ThyniusWeb.Server.Interfaces;
using ThyniusWeb.Server.Models;
using ThyniusWeb.Server.Models.Dtos;
using static ThyniusWeb.Server.Common.DataClasses;

namespace ThyniusWeb.Server.Controllers
{
    [Route("/assignment")]
    public class AssignmentController : Controller
    {
        IAssignmentRepository _assignmentRepository;
        IGroupRepository _groupRepository;
        UserManager<ApplicationUser> _userManager;
        IUserRepository _userRepository;
        IUserTextRepository _userTextRepository;
        public AssignmentController(IAssignmentRepository assignmentRepository, IUserRepository userRepository, IGroupRepository groupRepository, IUserTextRepository userTextRepository, UserManager<ApplicationUser> userManager)
        {
            _assignmentRepository = assignmentRepository;
            _groupRepository = groupRepository;
            _userManager = userManager;
            _userTextRepository = userTextRepository;
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
            return JsonSerializer.Serialize(new { assignment.Description, assignment.Name, assignment.DueDate, imageURL=_assignmentRepository.GetSignedAssignmentImgUrl(assignment)});
        }
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateAssignment([FromForm]CreateAssignmentDto model)
        {
            CreateAssignmentValidator validator = new CreateAssignmentValidator();
            if (!validator.Validate(model).IsValid)
            {
                return BadRequest();
            }

            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            Group group = await _groupRepository.GetByIdAsync(model.groupId);

            if(group == null)
            {
                return BadRequest();
            }
            if(group.ManagerId != callingUser.Id)
            {
                return Unauthorized();
            }


            Assignment newAssignment = new Assignment();
            newAssignment.Name = model.name;
            newAssignment.GroupId = model.groupId;
            newAssignment.DueDate = model.dueDate;
            newAssignment.Description = model.description;

            if (!_assignmentRepository.Add(newAssignment))
            {
                return StatusCode(500);
            }
            _assignmentRepository.UploadAssignmentPictureAsync(newAssignment, model.image);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> EditAssignment([FromForm]EditAssignmentDto model)
        {
            EditAssignmentValidator validator = new EditAssignmentValidator();
            if (!validator.Validate(model).IsValid)
            {
                return BadRequest();
            }

            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            Assignment assignment = await _assignmentRepository.GetByIdAsync(model.assignmentId);
            if(assignment == null)
            {
                return BadRequest();
            }

            Group group = await _groupRepository.GetByIdAsync(assignment.GroupId);

            if (group.ManagerId != callingUser.Id)
            {
                return Unauthorized();
            }

            assignment.Name = model.name;
            assignment.DueDate = model.dueDate;
            assignment.Description = model.description;

            if (!_assignmentRepository.Update(assignment))
            {
                return StatusCode(500);
            }
            if(model.image != null)
            {
                await _assignmentRepository.DeletAssignmentPictureAsync(assignment);
                _assignmentRepository.UploadAssignmentPictureAsync(assignment, model.image);
            }
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Teacher")]
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

            bool result = await _assignmentRepository.Delete(toDelete);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }


        [HttpGet]
        [Authorize]
        [Route("getuserassignments")]
        public async Task<string> getUserAssignments()
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            IEnumerable<Assignment> userAssignments = await _assignmentRepository.GetUserAssignmentsAsync(callingUser);
            return JsonSerializer.Serialize(userAssignments.Select(i => new {  i.Id,  i.Name, i.DueDate }));
        }
    }
}
