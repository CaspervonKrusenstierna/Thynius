﻿using FluentValidation;

namespace ThemisWeb.Server.Models.Dtos
{

    public class CreateAssignmentDto
    {
        public int groupId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime dueDate { get; set; }
        public IFormFile image { get; set; }
    }
    public class CreateAssignmentValidator : AbstractValidator<CreateAssignmentDto>
    {
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime)) && date > DateTime.Now;
        }
        public CreateAssignmentValidator()
        {
            RuleFor(p => p.groupId).NotNull();
            RuleFor(p => p.name).NotEmpty().MaximumLength(30).MinimumLength(6);
            RuleFor(p => p.description).NotEmpty().MaximumLength(2000);
            RuleFor(p => p.dueDate).NotEmpty().Must(BeAValidDate);
            RuleFor(p => p.image).NotEmpty();
        }
    }
}