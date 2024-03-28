﻿using Microsoft.AspNetCore.Authorization;
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
        ITextSessionRepository _textSessionRepository;
        public UserTextController(IUserTextRepository userTextRepository, ITextSessionRepository textSessionRepository, ISubmittmentRepository submittmentRepository, IUserRepository userRepository, IAssignmentRepository assignmentRepository, UserManager<ApplicationUser> userManager)
        {
            _submittmentRepository = submittmentRepository;
            _textSessionRepository = textSessionRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _assignmentRepository = assignmentRepository;
            _userTextRepository = userTextRepository;
        }

        [HttpPost]
        [Authorize(Roles = "VerifiedUser")]
        public async Task<IActionResult> CreateUserText(string TextTitle)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            UserText text = new UserText();
            text.Title = TextTitle;
            text.OwnerId = user.Id;
            
            _userTextRepository.Add(text);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserText(int textId)
        {
            UserText userText = await _userTextRepository.GetByIdAsync(textId);
            if (userText == null)
            {
                return BadRequest();
            }
            IEnumerable<TextSession> sessions = await _textSessionRepository.GetUserTextSessions(userText);

            foreach(TextSession session in sessions)
            {
                if (!_textSessionRepository.Delete(session))
                {
                    HttpContext.Response.StatusCode = 500;
                    return null;
                }
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
