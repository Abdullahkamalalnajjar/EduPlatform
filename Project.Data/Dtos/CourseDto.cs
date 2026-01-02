namespace Project.Data.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int EducationStageId { get; set; }
        public string EducationStageName { get; set; } = null!;
        public string CourseImageUrl { get; set; } = null!;

        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = null!;

        public ICollection<LectureDto> Lectures { get; set; } = new List<LectureDto>();
    }
    public class LectureDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public ICollection<MaterialDto> Materials { get; set; } = new List<MaterialDto>();
    }
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public bool IsFree { get; set; }
    }
}
