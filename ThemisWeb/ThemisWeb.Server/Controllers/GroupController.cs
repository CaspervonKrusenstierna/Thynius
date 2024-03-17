using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Controllers
{
    [Route("/groups")]
    [Authorize]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _classRepository;
        public GroupController(IGroupRepository classRepository) { 
            _classRepository = classRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup(string GroupName)
        {
            Group newGroup = new Group();
            newGroup.Name = GroupName;
            newGroup.
            if (!_classRepository.Add(newGroup))
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroup(int classId)
        {

            Group toDelete = await _classRepository.GetByIdAsync(classId);
            if(toDelete == null)
            {
                return BadRequest();
            }

            bool result = _classRepository.Delete(toDelete);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
