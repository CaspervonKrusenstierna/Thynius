using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Claims;
using ThemisWeb.Server.Repository;
using ThemisWeb.Server.Common;

namespace ThemisWeb.Server.Controllers
{
    [Route("group")]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public GroupController(IGroupRepository groupRepository, IAssignmentRepository assignmentRepository, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _assignmentRepository = assignmentRepository;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup(string GroupName, [FromBody] List<String> GroupUsers)
        {
            if (GroupName == null || GroupUsers == null)
            {
                return BadRequest();
            }

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            Group newGroup = new Group();
            newGroup.Name = GroupName;
            newGroup.ManagerId = user.Id;
            if (!_groupRepository.Add(newGroup))
            {
                return StatusCode(500);
            }
            foreach (string userId in GroupUsers)
            {
                ApplicationUser currUser = await _userRepository.GetByIdAsync(userId);
                if (currUser == null)
                {
                    return BadRequest();
                }
                if (currUser.OrganizationEmailExtension != currUser.OrganizationEmailExtension)
                {
                    return Unauthorized();
                }
                _userRepository.AddUserToGroup(currUser, newGroup);
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,OrganizationAdmin,Teacher")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Group toDelete = await _groupRepository.GetByIdAsync(groupId);

            if (toDelete == null)
            {
                return BadRequest();
            }
            if (toDelete.ManagerId != user.Id)
            {
                return Unauthorized();
            }

            bool result = _groupRepository.Delete(toDelete);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet]
        [Route("getusergroups")]
        [Authorize(Roles = "VerifiedUser")]
        public async Task<string> GetUserGroupIds(string userId)
        {
            IEnumerable<Group> Groups = await _groupRepository.GetUserGroups(userId);

            return System.Text.Json.JsonSerializer.Serialize(
            Groups.Select(group => (new GroupData { 
                Id = group.Id, 
                Name = group.Name
                //PictureLink = await _groupRepository.GetSignedGroupImgUrlAsync(group)
            })));
        }

        [HttpGet]
        [Route("getgroupinfo")]
        public async Task<string> GetGroupInfo(int groupId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Group group = await _groupRepository.GetByIdAsync(groupId);

            if (group == null)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }
            IEnumerable<ApplicationUser> groupUsers = await _userRepository.GetGroupUsers(group); // doing this should be fine because groups should not consist of many people

            if (!groupUsers.Contains(user) && !(group.ManagerId == user.Id))
            {
                HttpContext.Response.StatusCode = 401;
                return null;
            }

            IEnumerable<Assignment> groupAssignments = await _assignmentRepository.GetByGroupIdAsync(groupId);
            ApplicationUser Manager = await _userRepository.GetByIdAsync(group.ManagerId);

            GroupDataExtended dataToReturn = new GroupDataExtended { Id = group.Id, Name = group.Name, DateCreated = group.DateCreated };
            dataToReturn.userDatas = groupUsers.Select(i => new { i.Id, i.UserName });
            dataToReturn.assignmentDatas = groupAssignments.Select(i => new { i.Id, i.Name, i.DueDate });
            dataToReturn.ManagerData = new { Manager.Id, Manager.UserName };
            return System.Text.Json.JsonSerializer.Serialize(dataToReturn);
        }

    }
}
