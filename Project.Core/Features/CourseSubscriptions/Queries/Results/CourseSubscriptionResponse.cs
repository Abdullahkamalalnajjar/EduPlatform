namespace Project.Core.Features.CourseSubscriptions.Queries.Results
{
    public class CourseSubscriptionResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}