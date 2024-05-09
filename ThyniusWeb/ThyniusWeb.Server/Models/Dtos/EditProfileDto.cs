using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace ThyniusWeb.Server.Models.Dtos
{
    public class EditProfileDto
    {
        public string name { get; set; }
        public IFormFile profilePicture { get; set; }
    }
    public class EditProfileValidator : AbstractValidator<EditProfileDto>
    {
        public EditProfileValidator()
        {
            RuleFor(p => p.name).MinimumLength(8).MaximumLength(50);
        }
    }
}
