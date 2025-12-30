namespace Project.Core.Features.Teachers.Queries.Results
{
    public class TeacherByGradeSubjectResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public List<CourseDto> Courses { get; set; } = new();
    }

    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int GradeYear { get; set; }
        public List<LectureDto> Lectures { get; set; } = new();
    }

    public class LectureDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<MaterialDto> Materials { get; set; } = new();
    }

    public class MaterialDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
    }
}
