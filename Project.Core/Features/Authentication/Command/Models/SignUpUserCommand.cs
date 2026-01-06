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

        // teacher profile fields
        public string? PhoneNumber { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TelegramUrl { get; set; }
        public string? YouTubeChannelUrl { get; set; } // ✅ أضفنا هذا
        public string? WhatsAppNumber { get; set; }
        // profile photo file uploaded
        public IFormFile? PhotoFile { get; set; }

        public string? ParentPhoneNumberOfParent { get; set; } // parent

        public int? TeacherId { get; set; } // assistant
    }
}
