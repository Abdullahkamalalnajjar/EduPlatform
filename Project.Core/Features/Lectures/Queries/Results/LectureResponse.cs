namespace Project.Core.Features.Lectures.Queries.Results
{
    public class LectureResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int CourseId { get; set; }
    }
}