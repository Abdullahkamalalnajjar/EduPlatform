namespace Project.Data.Entities.People
{
    public class Teacher
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = null!;
        public Project.Data.Entities.Users.ApplicationUser User { get; set; } = null!;

        public int SubjectId { get; set; }
        public Project.Data.Entities.Curriculum.Subject Subject { get; set; } = null!;

        public ICollection<Project.Data.Entities.Curriculum.Course> Courses { get; set; } = new List<Project.Data.Entities.Curriculum.Course>();
    }
}