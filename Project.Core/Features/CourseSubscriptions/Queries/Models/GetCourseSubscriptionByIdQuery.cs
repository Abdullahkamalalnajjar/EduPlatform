namespace Project.Core.Features.CourseSubscriptions.Queries.Models
{
    public class GetCourseSubscriptionByIdQuery : IRequest<Response<CourseSubscriptionDto>>
    {
        public int Id { get; set; }
    }
}