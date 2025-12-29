using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.CourseSubscriptions.Commands.Models
{
    public class DeleteCourseSubscriptionCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
