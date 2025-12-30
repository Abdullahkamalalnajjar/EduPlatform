namespace Project.Data.Entities.People
{
    public class TeacherEducationStage
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public int EducationStageId { get; set; }
        public Project.Data.Entities.Curriculum.EducationStage EducationStage { get; set; } = null!;
    }
}
