namespace Project.Data.Entities.Curriculum
{
    public class EducationStage
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // e.g. "First Secondary", "Third Secondary"

        public ICollection<Project.Data.Entities.People.TeacherEducationStage> TeacherEducationStages { get; set; } = new List<Project.Data.Entities.People.TeacherEducationStage>();
    }
}
