namespace Project.Core.Features.Courses.Queries.Results
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int TeacherId { get; set; }
        public int EducationStageId { get; set; }
        public string EducationStageName { get; set; } = null!;
    }
}