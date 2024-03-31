using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Controllers
{
    [Route("/usertext")]
    public class UserTextController : Controller
    {
        IAssignmentRepository _assignmentRepository;
        IUserRepository _userRepository;
        UserManager<ApplicationUser> _userManager;
        ISubmittmentRepository _submittmentRepository;
        IUserTextRepository _userTextRepository;
        IGroupRepository _groupRepository;

        public UserTextController(IUserTextRepository userTextRepository, ISubmittmentRepository submittmentRepository, IUserRepository userRepository, IAssignmentRepository assignmentRepository, IGroupRepository groupRepository, UserManager<ApplicationUser> userManager)
        {
            _submittmentRepository = submittmentRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _assignmentRepository = assignmentRepository;
            _userTextRepository = userTextRepository;
            _groupRepository = groupRepository;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserText(int textId)
        {
            UserText userText = await _userTextRepository.GetByIdAsync(textId);
            if (userText == null)
            {
                return BadRequest();
            }


            if (_userTextRepository.Delete(userText))
            {
                return Ok();
            }
            HttpContext.Response.StatusCode = 500;
            return null;
        }

        [HttpGet]
        public Task<string> GetUserText(int textId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Authorize(Roles = "VerifiedUser")]
        public async Task<IActionResult> EditUserText(int charCount, int wordCount, [FromForm] IFormFile sessionData)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            UserText text = await _userTextRepository.GetByTextData(user, charCount, wordCount);
            if (text == null)
            {
                text = new UserText();
                text.OwnerId = user.Id;
                _userTextRepository.Add(text);
            }

            return Ok();
        }

        [HttpGet]
        [Route("getrawcontent")]
        [Authorize(Roles = "VerifiedUser")]
        public async Task<string> GetUserTextRawContent(int textId)
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            UserText textToAccess = await _userTextRepository.GetByIdAsync(textId);
            if(textToAccess == null)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }
            if(textToAccess.OwnerId != callingUser.Id)
            {
                HttpContext.Response.StatusCode = 401;
                return null;
            }
            throw new NotImplementedException();
        }

        [Route("/user/usertexts")]
        [HttpGet]
        [Authorize(Roles = "VerifiedUser")]
        public async Task<string> getUserTexts(string userId) {
            
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            ApplicationUser userToAccess = await _userRepository.GetByIdAsync(userId);
            if(callingUser != userToAccess)
            {
                HttpContext.Response.StatusCode = 401;
                return null;
            }
            IEnumerable<UserText> texts =  await _userTextRepository.GetUserTexts(userToAccess);
            return System.Text.Json.JsonSerializer.Serialize(texts.Select(i => new { i.Id, i.Title}));
        }
        
    }
}
