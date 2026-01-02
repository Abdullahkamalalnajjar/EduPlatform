using Project.Data.Entities.Curriculum;
using Project.Data.Entities.Users;

namespace Project.Data.Entities.People
{
    public class Teacher
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public ICollection<TeacherEducationStage> TeacherEducationStages { get; set; } = new List<TeacherEducationStage>();
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FacebookUrl { get; set; } = null!;
        public string TelegramUrl { get; set; } = null!;
        public string WhatsAppNumber { get; set; } = null!;
        public string? PhotoUrl { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}