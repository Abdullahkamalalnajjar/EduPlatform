namespace Project.Core.Features.CourseSubscriptions.Commands.Models
{
    public class CreateCourseSubscriptionCommand : IRequest<Response<int>>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
