using FluentValidation;
using PMWA.Application.Dtos.Task;

namespace PMWA.Application.Validators.Task
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Task name is required.")
                .MaximumLength(100).WithMessage("Task name must not exceed 100 characters.")
                .MinimumLength(10).WithMessage("Task name must be at least 10 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Task description must not exceed 500 characters.")
                .MinimumLength(100).WithMessage("Task description must be at least 100 characters");
        }
    }
}
