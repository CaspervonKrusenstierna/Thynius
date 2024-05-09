using FluentValidation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace ThyniusWeb.Server.Models.Dtos
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
            RuleFor(p => p.password).NotEmpty()
                .MinimumLength(8)
                .MaximumLength(24)
                .Matches(@"[A-Z]+")
                .Matches(@"[a-z]+")
                .Matches(@"[0-9]+");
        }
    }
}
