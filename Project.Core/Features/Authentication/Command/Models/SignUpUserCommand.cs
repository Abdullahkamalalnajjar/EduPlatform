using MediatR;
using Project.Core.Bases;
using Project.Data.Helpers;

namespace Project.Core.Features.Authentication.Command.Models
{
    public class SignUpUserCommand : IRequest<Response<AuthResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Role selection
        public string Role { get; set; } = "Student"; // Student/Teacher/Parent/Assistant

        // Role specific
        public int? GradeYear { get; set; } // student
        public string? ParentPhoneNumber { get; set; } // student

        public int? SubjectId { get; set; } // teacher
        public List<int> EducationStageIds { get; set; } = new List<int>(); // teacher may select multiple stages

        public string? NationalId { get; set; } // parent

        public int? TeacherId { get; set; } // assistant
    }
}
