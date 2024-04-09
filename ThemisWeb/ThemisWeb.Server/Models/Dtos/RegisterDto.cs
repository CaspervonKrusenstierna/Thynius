using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ThemisWeb.Server.Models.Dtos
{
    public class RegisterDto
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(p => p.email).EmailAddress();
            RuleFor(p => p.password).NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(24).WithMessage("Your password length must not exceed 24.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        }
    }
}
