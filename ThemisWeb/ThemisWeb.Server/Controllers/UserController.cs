using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ThemisWeb.Server.Data;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
namespace ThemisWeb.Server.Controllers
{
    [Route("/users")]
    [Authorize(Roles = "VerifiedUser")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        UserManager<ApplicationUser> _userManager;
        public UserController(IUserRepository userRepository,IGroupRepository groupRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("organizationusers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IEnumerable<string>> GetOrganizationUsers(string Organization)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.OrganizationEmailExtension != Organization)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return null;
            }
            IEnumerable<ApplicationUser> ToReturn = await _userRepository.GetOrganizationUsers(Organization);
            return ToReturn.Select(C => JsonConvert.SerializeObject(new UserData {ID = C.Id, Username = C.UserName}));
        }

        [HttpPost]
        [Route("addusertogroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddUserToGroup(string UserId, int GroupId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User); // user making request
            Group group = await _groupRepository.GetByIdAsync(GroupId);
            ApplicationUser userToAdd = await _userRepository.GetByIdAsync(UserId);
            if (userToAdd == null || group == null)
            {
                return BadRequest();
            }
            if(user.Id != group.ManagerId)
            {
                return Unauthorized();
            }

            bool result = _userRepository.AddUserToGroup(userToAdd, group);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("removeuserfromgroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveUserFromGroup(string UserId, int GroupId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User); // user making request
            Group group = await _groupRepository.GetByIdAsync(GroupId);
            ApplicationUser userToAdd = await _userRepository.GetByIdAsync(UserId);
            if (userToAdd == null || group == null)
            {
                return BadRequest();
            }
            if (user.Id != group.ManagerId)
            {
                return Unauthorized();
            }

            bool result = _userRepository.RemoveUserFromGroup(userToAdd, group);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet]
        [Route("groupids")]
        public async Task<IEnumerable<string>> GetGroupIds(int GroupId)
        {
            Group group = await _groupRepository.GetByIdAsync(GroupId);
            if (group == null)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }
            
            IEnumerable<ApplicationUser> ToReturn =  await _userRepository.GetGroupUsers(group);
            return ToReturn.Select(i => JsonConvert.SerializeObject(new UserData{ Username = i.UserName, ID = i.Id  }));
        }
    }
}
