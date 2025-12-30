namespace Project.Data.Entities.People
{
    public class Teacher
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = null!;
        public Project.Data.Entities.Users.ApplicationUser User { get; set; } = null!;
        // teacher may teach multiple education stages
        public ICollection<Project.Data.Entities.People.TeacherEducationStage> TeacherEducationStages { get; set; } = new List<Project.Data.Entities.People.TeacherEducationStage>();

        public int SubjectId { get; set; }
        public Project.Data.Entities.Curriculum.Subject Subject { get; set; } = null!;

        public ICollection<Project.Data.Entities.Curriculum.Course> Courses { get; set; } = new List<Project.Data.Entities.Curriculum.Course>();
    }
}