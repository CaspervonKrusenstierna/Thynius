using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ThemisWeb.Server.Common;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using static ThemisWeb.Server.Common.DataClasses;
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

        [HttpGet]
        [Route("/users/getsearchusers")]
        public async Task<string> GetSearchUsers(string Search, int Max=3, bool includeSelf = false)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.OrganizationEmailExtension;
            IEnumerable<ApplicationUser> SearchResult = await _userRepository.GetSearchUsers(Search,org,Max);
            if (includeSelf)
            {
                return JsonConvert.SerializeObject(SearchResult.Select(i => i.UserName));
            }
            return JsonConvert.SerializeObject(SearchResult.Where(i => i.UserName != user.UserName).Select(i => new { username=i.UserName, email=i.Email, id=i.Id }));
        }

    }
}
