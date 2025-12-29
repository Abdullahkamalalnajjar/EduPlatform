using FluentValidation;
using Project.Core.Features.Authentication.Command.Models;
using Project.Data.Consts;

namespace Project.Core.Features.Authentication.Command.Validators
{
    public class SignUpUserCommandValidator : AbstractValidator<SignUpUserCommand>
    {
        public SignUpUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required")
                .Must(BeAValidRole).WithMessage("Role must be one of: Student, Teacher, Parent, Assistant");

            When(x => string.Equals(x.Role, DefaultRoles.Student, StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.GradeYear).NotNull().WithMessage("GradeYear is required for student");
                RuleFor(x => x.ParentPhoneNumber).NotEmpty().WithMessage("ParentPhoneNumber is required for student");
            });

            When(x => string.Equals(x.Role, DefaultRoles.Teacher, StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.SubjectId).NotNull().WithMessage("SubjectId is required for teacher");
            });

            When(x => string.Equals(x.Role, DefaultRoles.Parent, StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.NationalId).NotEmpty().WithMessage("NationalId is required for parent");
            });

            When(x => string.Equals(x.Role, DefaultRoles.Assistant, StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.TeacherId).NotNull().WithMessage("TeacherId is required for assistant");
            });
        }

        private bool BeAValidRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return false;
            var allowed = new[] { DefaultRoles.Student, DefaultRoles.Teacher, DefaultRoles.Parent, DefaultRoles.Assistant };
            return allowed.Any(r => string.Equals(r, role, StringComparison.OrdinalIgnoreCase));
        }
    }
}
