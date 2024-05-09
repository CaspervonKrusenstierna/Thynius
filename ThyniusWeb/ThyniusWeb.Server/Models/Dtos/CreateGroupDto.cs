using FluentValidation;
using System.Drawing;

namespace ThyniusWeb.Server.Models.Dtos
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
            RuleFor(p => p.name).NotEmpty().MinimumLength(6).MaximumLength(48);
            RuleFor(p => p.image).NotEmpty();
        }
    }
}
