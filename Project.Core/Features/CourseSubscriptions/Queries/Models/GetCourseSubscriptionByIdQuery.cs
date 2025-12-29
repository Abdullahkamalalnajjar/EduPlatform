using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.CourseSubscriptions.Queries.Models
{
    public class GetCourseSubscriptionByIdQuery : IRequest<Response<Project.Core.Features.CourseSubscriptions.Queries.Results.CourseSubscriptionResponse>>
    {
        public int Id { get; set; }
    }
}