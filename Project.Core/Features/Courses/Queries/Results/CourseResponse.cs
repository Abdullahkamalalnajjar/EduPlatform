namespace Project.Core.Features.Courses.Queries.Results
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int GradeYear { get; set; }
        public int TeacherId { get; set; }
    }
}