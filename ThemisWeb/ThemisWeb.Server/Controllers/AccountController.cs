using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using System.Net;
using Microsoft.AspNetCore.Identity;
using ThemisWeb.Server.Repository;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using ThemisWeb.Server.Data;

namespace ThemisWeb.Server.Controllers
{
    [Authorize]
    [Route("/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOrganizationRepository organizationRepository, IUserRepository userRepository)
        {
            _userManager = userManager;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
        }

        [Route("initializeaccount")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InitializeAccount() // assign the user to an org to initialize the account
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string Email = user.NormalizedEmail;

            Organization org = await _organizationRepository.GetByEmailExtensionAsync(Email.Split("@")?[1]);
            if (org == null)
            {
                return StatusCode(404);
            }
            user.OrganizationEmailExtension = org.EmailExtension;
            _userRepository.Update(user);

            return StatusCode(200);
        }

        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [Route("getsessioninfo")]
        [HttpGet]
        public async Task<String> GetSessionInfo()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return JsonConvert.SerializeObject(new UserData{ ID = user.Id, Username = user.UserName });
        }
    }
}
