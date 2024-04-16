using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using System.Net;
using Microsoft.AspNetCore.Identity;
using ThemisWeb.Server.Repository;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using ThemisWeb.Server.Common;
using static ThemisWeb.Server.Common.DataClasses;
using static ThemisWeb.Server.Common.Utilities;
using ThemisWeb.Server.Models.Dtos;
using System.Text.Json;
using FluentValidation;
using Amazon.S3;
using Amazon.S3.Model;
using static System.Net.Mime.MediaTypeNames;

namespace ThemisWeb.Server.Controllers
{
    [Route("/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAmazonS3 _amazonS3;
        private readonly IConfiguration _configuration;
        public AccountController(IAmazonS3 amazonS3, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOrganizationRepository organizationRepository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _amazonS3 = amazonS3;
            _userManager = userManager;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        [Route("/logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        
        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterDto model)
        {
            RegisterValidator validator = new RegisterValidator();
            var validateResponse = validator.Validate(model);
            if (!validateResponse.IsValid)
            {
                return BadRequest();
            }

            string[] Split = model.email.Split("@");
            string Name = Split[0];
            string EmailExtension = Split[1];
            Organization org = await _organizationRepository.GetByEmailExtensionAsync(EmailExtension);
            if (org == null)
            {
                return BadRequest(System.Text.Json.JsonSerializer.Serialize(new {error="Organization is not valid."}));
            }

            ApplicationUser user = new ApplicationUser()
            {
                FullName = Name,
                Email = model.email,
                UserName = model.email,
                OrganizationEmailExtension = EmailExtension,
                PasswordHash = model.password,
            };
            var result = await _userManager.CreateAsync(user, user.PasswordHash!);
            var response = await _userRepository.UploadUserProfilePictureAsync(user, Utilities.GenerateInitialsImage(Split[0], Split[1]));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(System.Text.Json.JsonSerializer.Serialize(new { error = "Username is already taken." }));
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditProfile([FromForm] EditProfileDto model)
        {
            EditProfileValidator validator = new EditProfileValidator();
            var validateResponse = validator.Validate(model);
            if (!validateResponse.IsValid)
            {
                return BadRequest();
            }

            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            callingUser.PasswordHash = _userManager.PasswordHasher.HashPassword(callingUser, model.password);
            callingUser.FullName = model.name;
            _userRepository.UploadUserProfilePictureAsync(callingUser, (MemoryStream)model.profilePicture.OpenReadStream());
            _userRepository.Update(callingUser);
            return Ok();
        }

        [Route("/getsessioninfo")]
        [HttpGet]
        [Authorize]
        public async Task<String> GetSessionInfo()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            int roleLevel = await _userRepository.GetUserRoleLevel(user);
            return JsonConvert.SerializeObject(new UserData{ 
                ID = user.Id, 
                Username = user.UserName, 
                RoleLevel = roleLevel, 
                ProfilePictureUrl = await _userRepository.GetSignedUserProfileImgUrlAsync(user)
            });
        }

        [Route("/adduserrrole/organizationadmin")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrganizationAdminRole(string userId)
        {
            ApplicationUser user = await _userRepository.GetByIdAsync(userId);
            if(user == null)
            {
                return BadRequest();
            }
            await _userManager.AddToRoleAsync(user, "OrganizationAdmin");
            return Ok();
        }

        [Route("/adduserrrole/teacher")]
        [HttpPost]
        [Authorize(Roles = "Admin, OrganizationAdmin")]
        public async Task<IActionResult> AddTeacherRole(string userId)
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            ApplicationUser user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            if(callingUser.OrganizationEmailExtension != user.OrganizationEmailExtension)
            {
                return Unauthorized();
            }
            await _userManager.AddToRoleAsync(user, "Teacher");
            return Ok();
        }

        [Route("/installer")]
        [HttpGet]
        [Authorize]
        public String GetInstallerUrl()
        {
            GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = _configuration["BucketName"],
                Key = $"downloads/ThemisInstaller.msi",
                Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

            };
            return _amazonS3.GetPreSignedURL(getPreSignedUrlRequest);
        }
    }
}
