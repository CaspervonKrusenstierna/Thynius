using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ThemisWeb.Server.Common;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;
using ThemisWeb.Server.Models.Dtos;
using static ThemisWeb.Server.Common.DataClasses;

namespace ThemisWeb.Server.Controllers
{
    [Route("/usertext")]
    [Authorize]
    public class UserTextController : Controller
    {
        IAssignmentRepository _assignmentRepository;
        IUserRepository _userRepository;
        UserManager<ApplicationUser> _userManager;
        IUserTextRepository _userTextRepository;
        IGroupRepository _groupRepository;

        public UserTextController(IUserTextRepository userTextRepository, IUserRepository userRepository, IAssignmentRepository assignmentRepository, IGroupRepository groupRepository, UserManager<ApplicationUser> userManager)
        {
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
        [Authorize(Roles = "Admin, OrganizationAdmin, Teacher")]
        public async Task<string> GetUserText(int textId)
        {
            UserText text = await _userTextRepository.GetByIdAsync(textId);
            ApplicationUser textOwner = await _userRepository.GetByIdAsync(text.OwnerId);
            Assignment assignment = await _assignmentRepository.GetByIdAsync(textId);

            var detectionDataUrl = await _userTextRepository.S3GetDetectionDataSignedUrlAsync(text);
            return JsonSerializer.Serialize(new {textOwner.UserName, textOwner.Id, assignment.Name, detectionDataUrl});
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> SubmitTextSession([FromForm]SubmitTextSessionDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            UserText text = await _userTextRepository.GetByTextData(user, model.guid);
            if (text == null)
            {
                text = new UserText();
                text.OwnerId = user.Id;
                text.guid = model.guid;
                text.Title = "";
                _userTextRepository.Add(text);
            }
            string RawText = ThemistTextConverter.GetInputsRawText(model.sessionData);

            _userTextRepository.S3RawContentUpload(text, RawText);
            return Ok();
        }

        [HttpGet]
        [Route("getrawcontent")]
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

        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> SubmitUserText(int textId, int assignmentId)
        {
            UserText text = await _userTextRepository.GetByIdAsync(textId);
            if (text == null)
            {
                return BadRequest();
            }
            Assignment assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return BadRequest();
            }
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            IEnumerable<ApplicationUser> assignmentUsers = await _userRepository.GetGroupUsers(await _groupRepository.GetByIdAsync(assignment.GroupId));
            if (text.OwnerId != callingUser.Id || !assignmentUsers.Contains(callingUser))
            {
                /*return Unauthorized();*/
            }

            text.AssignmentId = assignmentId;

            _userTextRepository.Update(text);

            return Ok();
        }

        [HttpGet]
        [Route("/assignment/usertexts")]
        [Authorize(Roles = "Admin, OrganizationAdmin, Teacher")]
        public async Task<string> GetAssignmentUserTexts(int assignmentId, int pageIndex, int pageSize)
        {
            Assignment assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            IEnumerable<Assignment> userAssignments = await _assignmentRepository.GetUserAssignmentsAsync(callingUser);
            Group assignmentGroup = await _groupRepository.GetAssignmentGroup(assignment);

            if (!userAssignments.Contains(assignment) && !(assignmentGroup.ManagerId == callingUser.Id))
            {
                HttpContext.Response.StatusCode = 401;
                return null;
            }

            IEnumerable<UserText> userTexts = await _userTextRepository.GetAssignmentTextsPage(assignment, pageIndex, pageSize);
            List<dynamic> userData = [];
            foreach (var userText in userTexts)
            {
                var user = await _userRepository.GetByIdAsync(userText.OwnerId);

                userData.Add(new { userText.Id, user.UserName, userText.WarningLevel });
            }
            return JsonSerializer.Serialize(userData);
        }

        [HttpGet]
        [Route("/assignment/missingusertexts")]
        [Authorize(Roles = "Admin, OrganizationAdmin, Teacher")]
        public async Task<string> GetAssignmentMissingUserTexts(int assignmentId)
        {
            ApplicationUser callingUser = await _userManager.GetUserAsync(HttpContext.User);
            Assignment assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
            Group group = await _groupRepository.GetByIdAsync(assignment.GroupId);
            IEnumerable<ApplicationUser> groupUsers = await _userRepository.GetGroupUsers(group);

            List<ApplicationUser> nonSubmittedUsers = [];
            foreach (ApplicationUser user in groupUsers)
            {
                bool hasSubmitted = false;
                IEnumerable<UserText> userTexts = await _userTextRepository.GetUserTexts(user);
                foreach (UserText text in userTexts)
                {
                    if(text.AssignmentId == assignmentId)
                    {
                        hasSubmitted = true;
                    }
                }
                if(!hasSubmitted)
                {
                    nonSubmittedUsers.Add(user);
                }
            }
            return JsonSerializer.Serialize(nonSubmittedUsers.Select(i => new {i.Id, i.UserName}));
        }
    }
}
