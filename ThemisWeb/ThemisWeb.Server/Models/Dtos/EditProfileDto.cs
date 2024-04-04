using FluentValidation;

namespace ThemisWeb.Server.Models.Dtos
{
    public class EditProfileDto
    {
        public string name { get; set; }
        public string password { get; set; }
        public IFormFile profilePicture { get; set; }
    }
    public class EditProfileValidator : AbstractValidator<EditProfileDto>
    {
        public EditProfileValidator()
        {
            RuleFor(p => p.name).MinimumLength(8).WithMessage("Your name length must be atleast 4.").MaximumLength(50).WithMessage("Your name must not exceed 50 characters.");
            RuleFor(p => p.password).NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        }
    }
}
