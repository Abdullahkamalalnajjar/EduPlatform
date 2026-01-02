namespace Project.Core.Features.CourseSubscriptions.Commands.Models
{
    public class EditCourseSubscriptionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string? Status { get; set; }
    }
}
