using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThyniusWeb.Server.Interfaces;
using ThyniusWeb.Server.Models;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Claims;
using ThyniusWeb.Server.Repository;
using ThyniusWeb.Server.Common;
using static ThyniusWeb.Server.Common.DataClasses;
using ThyniusWeb.Server.Models.Dtos;
using FluentValidation;

namespace ThyniusWeb.Server.Controllers
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
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateGroup([FromForm]CreateGroupDto model)
        {
            CreateGroupValidator validator = new CreateGroupValidator();
            if (!validator.Validate(model).IsValid)
            {
                return BadRequest();
            }

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            Group newGroup = new Group();
            newGroup.Name = model.name;
            newGroup.ManagerId = user.Id;

            if (!_groupRepository.Add(newGroup))
            {
                HttpContext.Response.StatusCode = 500;
                return null;
            }

            if(model.users != null)
            {
                foreach (string userId in model.users)
                {
                    ApplicationUser currUser = await _userRepository.GetByIdAsync(userId);
                    if (currUser == null)
                    {
                        _groupRepository.Delete(newGroup);
                        return BadRequest();
                    }
                    if (currUser.OrganizationEmailExtension != currUser.OrganizationEmailExtension)
                    {
                        HttpContext.Response.StatusCode = 401;
                        _groupRepository.Delete(newGroup);
                        return Unauthorized();
                    }
                    _userRepository.AddUserToGroup(currUser, newGroup);
                }
            }

            await _groupRepository.UploadGroupPictureAsync(newGroup, model.image);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> EditGroup([FromForm]EditGroupDto model)
        {
            EditGroupValidator validator = new EditGroupValidator();
            if (!validator.Validate(model).IsValid)
            {
                return BadRequest();
            }

            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            Group group = await _groupRepository.GetByIdAsync(model.id);
            if(group == null)
            {
                return BadRequest();
            }
            if(group.ManagerId != callingUser.Id)
            {
                return Unauthorized();
            }

            group.Name = model.name;
            if (model.image != null)
            {
                await _groupRepository.DeleteGroupPictureAsync(group);
                await _groupRepository.UploadGroupPictureAsync(group, model.image);
            }

            IEnumerable<ApplicationUser> OldGroupUsers = await _userRepository.GetGroupUsers(group);
            List<ApplicationUser> NewGroupUsers = [];

            if(model.users == null)
            {
                foreach (ApplicationUser olduser in OldGroupUsers)
                {
                  _userRepository.RemoveUserFromGroup(olduser, group);
                }
            }
            else
            {
                foreach (string userId in model.users)
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
                    NewGroupUsers.Add(currUser);
                }

                foreach (ApplicationUser olduser in OldGroupUsers)
                {
                    if (!NewGroupUsers.Contains(olduser))
                    {
                        _userRepository.RemoveUserFromGroup(olduser, group);
                    }
                }
                foreach (ApplicationUser newUser in NewGroupUsers)
                {
                    IEnumerable<Group> userGroups = await _groupRepository.GetUserGroups(newUser.Id);
                    if (!userGroups.Contains(group))
                    {
                        _userRepository.AddUserToGroup(newUser, group);
                    }
                }
            }
            _groupRepository.Update(group);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            Group toDelete = await _groupRepository.GetByIdAsync(groupId);

            if (toDelete == null)
            {
                return BadRequest();
            }
            if (toDelete.ManagerId != callingUser.Id)
            {
                return Unauthorized();
            }
            IEnumerable<Assignment> groupAssignments = await _assignmentRepository.GetByGroupIdAsync(toDelete.Id);
            foreach (Assignment assignment in groupAssignments)
            {
                await _assignmentRepository.Delete(assignment);
            }
            bool result = await _groupRepository.Delete(toDelete);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<string> GetGroupInfo(int groupId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Group group = await _groupRepository.GetByIdAsync(groupId);

            if (group == null)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }
            IEnumerable<ApplicationUser> groupUsers = await _userRepository.GetGroupUsers(group);  //doing this should be fine because groups should not consist of many people

            if (!groupUsers.Contains(user) && !(group.ManagerId == user.Id))
            {
                HttpContext.Response.StatusCode = 401;
                return null;
            }

            IEnumerable<Assignment> groupAssignments = await _assignmentRepository.GetByGroupIdAsync(groupId);
            ApplicationUser Manager = await _userRepository.GetByIdAsync(group.ManagerId);

            GroupDataExtended dataToReturn = new GroupDataExtended {Name = group.Name, DateCreated = group.DateCreated };
            dataToReturn.assignmentDatas = groupAssignments.Select(i => new {i.Id, i.Name, i.DueDate, Image=_assignmentRepository.GetSignedAssignmentImgUrl(i)});
            dataToReturn.ManagerData = new { Manager.Id, Manager.UserName };
            return System.Text.Json.JsonSerializer.Serialize(dataToReturn);
        }

        [HttpGet]
        [Route("getusergroups")]
        [Authorize]
        public async Task<string> GetUserGroups(string userId)
        {
            IEnumerable<Group> Groups = await _groupRepository.GetUserGroups(userId);

            return System.Text.Json.JsonSerializer.Serialize(Groups.Select(i => new { PictureLink=_groupRepository.GetSignedGroupImgUrl(i), i.Id, i.Name, i.ManagerId}));
        }
    }
}
