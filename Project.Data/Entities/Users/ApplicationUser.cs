using Microsoft.AspNetCore.Identity;
using Project.Data.Entities.People;

namespace Project.Data.Entities.Users
{
    public sealed class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public bool IsDisable { get; set; } = false;

        public List<RefreshToken>? RefreshTokens { get; set; } = new List<RefreshToken>();

        // Navigation Properties لكل نوع مستخدم
        public Student? StudentProfile { get; set; }
        public Parent? ParentProfile { get; set; }
        public Teacher? TeacherProfile { get; set; }
        public Assistant? AssistantProfile { get; set; }
    }
}
