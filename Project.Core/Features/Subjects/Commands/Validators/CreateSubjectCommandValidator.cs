using FluentValidation;
using Project.Core.Features.Subjects.Commands.Models;

namespace Project.Core.Features.Subjects.Commands.Validators
{
    public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}