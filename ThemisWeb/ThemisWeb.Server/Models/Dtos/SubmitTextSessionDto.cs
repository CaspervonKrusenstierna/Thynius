using System.Diagnostics.CodeAnalysis;

namespace ThemisWeb.Server.Models.Dtos
{
    public class SubmitTextSessionDto
    {
        [NotNull]
        public UInt64 guid { get; set; }

        [NotNull]
        public IFormFile sessionData { get; set; }
    }
}
