using FluentValidation;

namespace ThyniusWeb.Server.Models.Dtos
{

    public class EditAssignmentDto
    {
        public int assignmentId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime dueDate { get; set; }
        public IFormFile image { get; set; }
    }
    public class EditAssignmentValidator : AbstractValidator<EditAssignmentDto>
    {
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime)) && date > DateTime.Now;
        }
        public EditAssignmentValidator()
        {
            RuleFor(p => p.assignmentId).NotNull();
            RuleFor(p => p.name).NotEmpty().MaximumLength(30).MinimumLength(6);
            RuleFor(p => p.description).NotEmpty().MaximumLength(2000);
            RuleFor(p => p.dueDate).NotEmpty().Must(BeAValidDate);
        }
    }
}