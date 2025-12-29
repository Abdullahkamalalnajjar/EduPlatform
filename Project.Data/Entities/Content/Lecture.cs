namespace Project.Data.Entities.Content
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public int CourseId { get; set; }
        public Project.Data.Entities.Curriculum.Course Course { get; set; } = null!;

        public ICollection<Project.Data.Entities.Content.LectureMaterial> Materials { get; set; } = new List<Project.Data.Entities.Content.LectureMaterial>();
        public Project.Data.Entities.Exams.Exam? Exam { get; set; }
    }
}