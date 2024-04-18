using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ThyniusWeb.Server.Common;
using ThyniusWeb.Server.Interfaces;
using ThyniusWeb.Server.Models;
using ThyniusWeb.Server.Models.Dtos;
using ThyniusWeb.Server.Repository;
using static ThyniusWeb.Server.Common.DataClasses;
namespace ThyniusWeb.Server.Controllers
{
    [Route("/users")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        UserManager<ApplicationUser> _userManager;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IOrganizationRepository _organizationRepository;
        public UserController(IUserRepository userRepository, IOrganizationRepository organizationRepository, IAssignmentRepository assignmentRepository, IGroupRepository groupRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _userManager = userManager;
            _assignmentRepository = assignmentRepository;
            _organizationRepository = organizationRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, OrganizationAdmin, Teacher")]
        [Route("searchusers")]
        public async Task<string> GetSearchUsers(string Search, int Max=3, bool includeSelf = false)
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            string org = callingUser.OrganizationEmailExtension;
            IEnumerable<ApplicationUser> SearchResult = await _userRepository.GetSearchUsers(Search,org,Max);
            if (includeSelf)
            {
                return JsonConvert.SerializeObject(SearchResult.Select(i => i.UserName));
            }
            return JsonConvert.SerializeObject(SearchResult.Where(i => i.UserName != callingUser.UserName).Select(i => new { username=i.UserName, email=i.Email, id=i.Id }));
        }

        [HttpGet]
        [Route("groupusers")]
        [Authorize]
        public async Task<string> GetGroupUsers(int groupId)
        {
            Group group = await _groupRepository.GetByIdAsync(groupId);
            var groupUsers = await _userRepository.GetGroupUsers(group);
            return System.Text.Json.JsonSerializer.Serialize(groupUsers.Select(i => new {username=i.UserName, i.Id}));
        }

        [HttpPost]
        [Route("/organization/organizationteachers")]
        [Authorize(Roles = "OrganizationAdmin")]
        public async Task<IActionResult> SetOrganizationTeachers([FromForm] SetOrganizationTeachersDto model)
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);

            IEnumerable<ApplicationUser> OldTeachers = await _userRepository.getOrganizationTeachers(callingUser.OrganizationEmailExtension);
            List<ApplicationUser> NewTeachers = [];
            if (model.Teachers == null)
            {
                foreach (ApplicationUser oldTeacher in OldTeachers)
                {
                    await _userManager.RemoveFromRoleAsync(oldTeacher, "Teacher");
                }
            }
            else
            {
                foreach (string teacherId in model.Teachers)
                {
                    ApplicationUser currTeacher = await _userRepository.GetByIdAsync(teacherId);
                    if (currTeacher == null)
                    {
                        return BadRequest();
                    }
                    if (currTeacher.OrganizationEmailExtension != currTeacher.OrganizationEmailExtension)
                    {
                        return Unauthorized();
                    }
                    NewTeachers.Add(currTeacher);
                }

                foreach (ApplicationUser oldTeacher in OldTeachers)
                {
                    if (!NewTeachers.Contains(oldTeacher))
                    {
                        await _userManager.RemoveFromRoleAsync(oldTeacher, "Teacher");
                    }
                }
                foreach (ApplicationUser newTeacher in NewTeachers)
                {
                    await _userManager.AddToRoleAsync(newTeacher, "Teacher");
                }
            }
            return Ok();
        }

        [HttpGet]
        [Route("/organization/organizationteachers")]
        [Authorize(Roles = "OrganizationAdmin")]
        public async Task<string> GetOrganizationTeachers()
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            var teachers = await _userRepository.getOrganizationTeachers(callingUser.OrganizationEmailExtension);
            

            return System.Text.Json.JsonSerializer.Serialize(teachers.Select(i => new { username = i.UserName, email = i.Email, id = i.Id }));
        }
    }
}
