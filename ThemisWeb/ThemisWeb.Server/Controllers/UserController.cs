using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Controllers
{
    [Route("/users")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        UserManager<ApplicationUser> _userManager;
        public UserController(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/byorganization")]
        public async Task<IEnumerable<ApplicationUser>> GetOrganizationUsers(string Organization)
        {
            IEnumerable<ApplicationUser> ToReturn = await _userRepository.GetOrganizationUsers(Organization);
            return ToReturn;
        }

        [HttpGet]
        [Route("/namesbyorganization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IEnumerable<string>> GetOrganizationUsernames(string Organization)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.OrganizationEmailExtension != Organization)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return null;
            }

            IEnumerable<ApplicationUser> ToReturn = await _userRepository.GetOrganizationUsers(Organization);
            return ToReturn.Select(C => C.Email);
        }

        [HttpPost]
        [Route("/addusertogroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> AddUserToGroup(string UserId, int GroupId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            return Ok();
        }
    }
}
