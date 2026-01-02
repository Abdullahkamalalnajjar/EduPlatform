namespace Project.Core.Features.CourseSubscriptions.Queries.Models
{
    public class GetCourseSubscriptionByStudentAndStatusQuery : IRequest<Response<IEnumerable<CourseSubscriptionDto>>>
    {
        public GetCourseSubscriptionByStudentAndStatusQuery(int studentId, string status)
        {
            StudentId = studentId;
            Status = status;
        }

        public int StudentId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
