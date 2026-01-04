using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.CourseSubscriptions.Queries.Models
{
    public class GetStudentSubscriptionsByTeacherQuery : IRequest<Response<IEnumerable<CourseSubscriptionDto>>>
    {
        public int TeacherId { get; set; }
    }
}
