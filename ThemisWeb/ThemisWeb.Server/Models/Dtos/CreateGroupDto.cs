using FluentValidation;
using System.Drawing;

namespace ThemisWeb.Server.Models.Dtos
{
    public class CreateGroupDto
    {
        public string name { get; set; }
        public List<String> users { get; set; }
        public IFormFile image { get; set; }
    }
    public class CreateGroupValidator : AbstractValidator<CreateGroupDto>
    {
        public CreateGroupValidator()
        {
            RuleFor(p => p.name).NotEmpty();
            RuleFor(p => p.image).NotEmpty();
        }
    }
}
