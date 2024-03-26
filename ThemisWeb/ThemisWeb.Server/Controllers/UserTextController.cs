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
        public async Task<IActionResult> CreateUserText(string TextTitle, int SessionId)
        {
            TextSession session = await _textSessionRepository.GetByIdAsync(SessionId);
            if (session == null)
            {
                return BadRequest();
            }
            
            UserText text = new UserText();
            text.Title = TextTitle;
            
            _userTextRepository.Add(text);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditUserText(int UserTextId, string TextTitle)
        {
            UserText text = await _userTextRepository.GetByIdAsync(UserTextId);
            if (text == null)
            {
                return BadRequest();
            }

            if(TextTitle != text.Title)
            {
                text.Title = TextTitle;
                _userTextRepository.Update(text);
            }

            //calc new raw data

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
            if (_userTextRepository.Delete(userText))
            {
                return Ok();
            }
            HttpContext.Response.StatusCode = 500;
            return null;
        }

        [HttpGet]
        public IActionResult GetUserText()
        {
            throw new NotImplementedException();
        }

        [Route("usertexts")]
        [HttpGet]
        [Authorize(Roles = "VerifiedUser")]
        public async Task<IEnumerable<UserText>> getUserTexts() { 
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            return await _userTextRepository.GetUserTexts(callingUser);
        }

    }
}