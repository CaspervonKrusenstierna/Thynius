using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ThyniusWeb.Server.Common;
using ThyniusWeb.Server.Interfaces;
using ThyniusWeb.Server.Models;
using ThyniusWeb.Server.Models.Dtos;
using ThyniusWeb.Server.Common;
using static ThyniusWeb.Server.Common.DataClasses;
using static ThyniusWeb.Server.Common.ThyniustTextConverter;
using Amazon.S3.Model;

namespace ThyniusWeb.Server.Controllers
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

            IEnumerable<Input> sessionInputs = ReadInputs(model.sessionData);
            if (text == null)
            {
                text = new UserText();
                text.OwnerId = user.Id;
                text.guid = model.guid;

                string RawText = GetInputsRawText(sessionInputs);

                text.Title = ThyniustTextConverter.GetTextTitle(RawText);
                _userTextRepository.Add(text);

                await _userTextRepository.S3RawContentUpload(text, RawText);

                Stream stream = model.sessionData.OpenReadStream();
                await _userTextRepository.S3InputDataUpload(text, stream);
                stream.Dispose();
                return Ok();
            }
            else
            {

                GetObjectResponse previousInputsRes = await _userTextRepository.S3GetInputDataAsync(text);
                Stream newInputs = model.sessionData.OpenReadStream();

                MemoryStream output = new MemoryStream();

                previousInputsRes.ResponseStream.CopyTo(output);
                newInputs.CopyTo(output);

                string RawText = GetInputsRawText(ReadInputsStream(output));
                await _userTextRepository.S3RawContentDelete(text);
                await _userTextRepository.S3RawContentUpload(text, RawText);
                text.Title = ThyniustTextConverter.GetTextTitle(RawText);
                _userTextRepository.Update(text);

                await _userTextRepository.S3InputDataDelete(text);
                await _userTextRepository.S3InputDataUpload(text, output);
                previousInputsRes.ResponseStream.Dispose();
                newInputs.Dispose();
                output.Dispose();

            }

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, OrganizationAdmin, Teacher")]
        public async Task<string> GetSubmittmentInfo(int textId)
        {
            UserText text = await _userTextRepository.GetByIdAsync(textId);
            if(text == null)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }
            return JsonSerializer.Serialize(new { 
                inputDataURL = _userTextRepository.S3GetInputDataSignedUrl(text),
                rawTextURL = _userTextRepository.S3GetRawContentSignedUrl(text),
                detectionDataURL = _userTextRepository.S3GetDetectionDataSignedUrl(text)
            });
        }

        [HttpGet]
        [Route("rawcontent")]
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
            return JsonSerializer.Serialize(new { rawdatalink = _userTextRepository.S3GetRawContentSignedUrl(textToAccess)});
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
            texts = texts.Where(s => s.AssignmentId == null);
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


            ThyniusDetector detector = new ThyniusDetector(_userTextRepository, callingUser);
            await detector.Analyze(text);
            text.WarningLevel = detector.GetDetectionScore();
            _userTextRepository.Update(text);

            await _userTextRepository.S3DetectionDataUpload(text, Newtonsoft.Json.JsonConvert.SerializeObject(detector.getDetectionData()));

            return Ok();
        }

        [HttpGet]
        [Route("/assignment/usertexts")]
        [Authorize(Roles = "Admin, OrganizationAdmin, Teacher")]
        public async Task<string> GetAssignmentUserTexts(int assignmentId)
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

            IEnumerable<UserText> userTexts = await _userTextRepository.GetAssignmentTexts(assignment);
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
