namespace Project.Data.Entities.Curriculum
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int EducationStageId { get; set; }
        public EducationStage EducationStage { get; set; } = null!;

        public string CourseImageUrl { get; set; } = null!;

        public int TeacherId { get; set; }
        public Project.Data.Entities.People.Teacher Teacher { get; set; } = null!;

        public ICollection<Project.Data.Entities.Content.Lecture> Lectures { get; set; } = new List<Project.Data.Entities.Content.Lecture>();
    }
}