﻿using FluentValidation;

namespace ThemisWeb.Server.Models.Dtos
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
            RuleFor(p => p.name).NotEmpty();
            RuleFor(p => p.id).NotNull();
        }
    }
}