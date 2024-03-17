using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using System.Net;
using Microsoft.AspNetCore.Identity;
using ThemisWeb.Server.Repository;
using Microsoft.AspNetCore.Authorization;

namespace ThemisWeb.Server.Controllers
{

    [Route("/organization")]
    [Authorize]
    public class OrganizationController : Controller
    {
        IOrganizationRepository _organizationRepository;
        IUserRepository _userRepository;
        UserManager<ApplicationUser> _userManager;
        public OrganizationController(IOrganizationRepository organizationRepository, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this._organizationRepository = organizationRepository;
            this._userRepository = userRepository;
            this._userManager = userManager;
        }

        [Authorize]
        [Route("/InitializeAccount")]
        [HttpPatch]
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

        [HttpPost]
        public async Task<IActionResult> CreateOrganization(string EmailExtension)
        {
            Organization org = await _organizationRepository.GetByEmailExtensionAsync(EmailExtension);
            if (org != null || EmailExtension == null)
            {
                return StatusCode(400);
            }


            org = await _organizationRepository.GetByEmailExtensionAsync(EmailExtension);
            if (org == null)
            {
                org = new Organization();
                org.EmailExtension = EmailExtension;
                if (!_organizationRepository.Add(org))
                {
                    return StatusCode(500);
                }
                return StatusCode(200);
            }

            return StatusCode(400);
        }

    }
}
