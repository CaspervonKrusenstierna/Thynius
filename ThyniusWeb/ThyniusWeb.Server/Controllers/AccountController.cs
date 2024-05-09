
using Microsoft.AspNetCore.Mvc;
using ThyniusWeb.Server.Interfaces;
using ThyniusWeb.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using ThyniusWeb.Server.Common;
using static ThyniusWeb.Server.Common.DataClasses;
using ThyniusWeb.Server.Models.Dtos;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Localization;
using static ThyniusWeb.Server.Common.Utilities;

namespace ThyniusWeb.Server.Controllers
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
        private readonly IStringLocalizer<AccountController> _localizer;
        public AccountController(IAmazonS3 amazonS3, IStringLocalizer<AccountController> localizer, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOrganizationRepository organizationRepository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _amazonS3 = amazonS3;
            _userManager = userManager;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _signInManager = signInManager;
            _localizer = localizer;
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

            EmailInfo emailInfo = GetEmailInfo(model.email);
            if (!emailInfo.success)
            {
                return BadRequest(System.Text.Json.JsonSerializer.Serialize(new { error = _localizer["Invalid email."].Value }));
            }
            Organization org = await _organizationRepository.GetByEmailExtensionAsync(emailInfo.organization);
            if (org == null)
            {
                return BadRequest(System.Text.Json.JsonSerializer.Serialize(new {error=_localizer["Invalid organization."].Value}));
            }

            ApplicationUser user = new ApplicationUser()
            {
                FullName = emailInfo.firstName + " " + emailInfo.lastName,
                Email = model.email,
                UserName = model.email,
                OrganizationEmailExtension = emailInfo.organization,
                PasswordHash = model.password,
            };
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            //await _userRepository.UploadUserProfilePictureAsync(user, Utilities.GenerateInitialsImage(emailInfo.firstName, emailInfo.lastName));
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(System.Text.Json.JsonSerializer.Serialize(new { error = _localizer["Username is already taken."].Value}));
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
            callingUser.FullName = model.name;
            if(model.profilePicture != null)
            {
                MemoryStream s = new MemoryStream();
                model.profilePicture.CopyTo(s);
                await _userRepository.DeleteUserProfilePictureAsync(callingUser);
                await _userRepository.UploadUserProfilePictureAsync(callingUser, s);
            }
            _userRepository.Update(callingUser);
            return Ok();
        }

        [Route("/getsessioninfo")]
        [HttpGet]
        public async Task<String> GetSessionInfo()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                Response.StatusCode = 204;
                return "";
            }
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            int roleLevel = await _userRepository.GetUserRoleLevel(user);
            return JsonConvert.SerializeObject(new UserData{ 
                ID = user.Id, 
                Username = user.FullName, 
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
        public async Task<IActionResult> AddTeacherRole(string userId, string email)
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            ApplicationUser user;
            if (userId != null)
            {
                user = await _userRepository.GetByIdAsync(userId);
            }
            else
            {
                user = await _userRepository.GetByEmailAsync(email);
            }
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
                Key = $"downloads/ThyniusInstaller.msi",
                Expires = DateTime.Today.AddHours(DateTime.Now.Hour + 1)

            };
            return _amazonS3.GetPreSignedURL(getPreSignedUrlRequest);
        }
    }
}
