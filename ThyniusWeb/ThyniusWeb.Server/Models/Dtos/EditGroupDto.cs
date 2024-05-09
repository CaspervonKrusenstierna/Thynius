using FluentValidation;

namespace ThyniusWeb.Server.Models.Dtos
{
    public class EditGroupDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<String> users { get; set; }
        public IFormFile image { get; set; }
    }

    public class EditGroupValidator : AbstractValidator<EditGroupDto>
    {
        public EditGroupValidator()
        {
            RuleFor(p => p.name).MinimumLength(6).MaximumLength(48);
            RuleFor(p => p.id).NotNull();
        }
    }
}
