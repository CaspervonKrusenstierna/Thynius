using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Controllers
{
    public class TextSessionController : Controller
    {
        private readonly ITextSessionRepository _textSessionRepository;
        public TextSessionController(ITextSessionRepository textSessionRepository) {
            _textSessionRepository = textSessionRepository;
        }

        [HttpPost]
        public int CreateTextSession(int TextId, [FromBody] IFormFile initialFormFile)
        {
            TextSession textSession = new TextSession();
            textSession.TextId = TextId;
            _textSessionRepository.Add(textSession);
            //_textSessionRepository.S3Upload(textSession, formFile);
            return TextId;
        }

        [HttpPut]
        public async Task<IActionResult> EditTextSession(int sessionId, IFormFile formFile)
        {
            TextSession textSession = await _textSessionRepository.GetByIdAsync(sessionId);
            if(textSession == null)
            {
                return BadRequest();
            }
            //_textSessionRepository.S3Upload(textSession, formFile);
            return Ok();
            throw new NotImplementedException();
        }

    }
}
