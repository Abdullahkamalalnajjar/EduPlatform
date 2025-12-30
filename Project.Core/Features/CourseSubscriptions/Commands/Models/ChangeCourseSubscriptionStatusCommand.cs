namespace Project.Core.Features.CourseSubscriptions.Commands.Models
{
    public class ChangeCourseSubscriptionStatusCommand : IRequest<Response<int>>
    {
        public ChangeCourseSubscriptionStatusCommand(int id, string status)
        {
            Id = id;
            Status = status;
        }

        public int Id { get; set; }
        // Expected values: "Pending", "Approved", "Rejected"
        public string Status { get; set; } = null!;
    }
}
