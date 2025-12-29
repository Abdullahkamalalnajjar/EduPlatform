using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.CourseSubscriptions.Commands.Models
{
    public class ChangeCourseSubscriptionStatusCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        // Expected values: "Pending", "Approved", "Rejected"
        public string Status { get; set; } = null!;
    }
}
