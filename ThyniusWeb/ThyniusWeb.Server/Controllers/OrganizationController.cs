using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ThyniusWeb.Server.Interfaces;
using ThyniusWeb.Server.Models;
using System.Net;
using Microsoft.AspNetCore.Identity;
using ThyniusWeb.Server.Repository;
using Microsoft.AspNetCore.Authorization;
using ThyniusWeb.Server.Models.Dtos;
using System.Text.Json;

namespace ThyniusWeb.Server.Controllers
{

    [Route("organization")]
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrganization(string EmailExtension)
        {
            Organization org = await _organizationRepository.GetByEmailExtensionAsync(EmailExtension);
            if (org == null)
            {
                return StatusCode(400);
            }
            bool result = _organizationRepository.Delete(org);
            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
