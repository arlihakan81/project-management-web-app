using FluentValidation;
using PMWA.Application.Dtos.Project;

namespace PMWA.Application.Validators.Project
{
    public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Project name is required.")
                .MaximumLength(100).WithMessage("Project name must not exceed 100 characters.")
                .MinimumLength(10).WithMessage("Project name must be at least 10 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Project description must not exceed 500 characters.")
                .MinimumLength(100).WithMessage("Project description must be at least 100 characters");
        }
    }
}
