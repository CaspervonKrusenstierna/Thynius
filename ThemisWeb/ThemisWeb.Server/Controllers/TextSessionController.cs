using Microsoft.AspNetCore.Mvc;
using ThemisWeb.Server.Interfaces;
using ThemisWeb.Server.Models;

namespace ThemisWeb.Server.Controllers
{
    [Route("/textsession")]
    public class TextSessionController : Controller
    {
        private readonly ITextSessionRepository _textSessionRepository;
        public TextSessionController(ITextSessionRepository textSessionRepository) {
            _textSessionRepository = textSessionRepository;
        }

        [HttpPost]
        public int CreateTextSession(int TextId, [FromBody] IFormFile FormFile)
        {
            TextSession textSession = new TextSession();
            textSession.TextId = TextId;
            _textSessionRepository.Add(textSession);
            //_textSessionRepository.S3Upload(textSession, formFile);
            return TextId;
        }

    }
}
