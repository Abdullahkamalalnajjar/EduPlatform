namespace Project.Core.Features.CourseSubscriptions.Queries.Models
{
    public class GetCoursesByStudentQuery : IRequest<Response<IEnumerable<Project.Data.Dtos.CourseSubscriptionDto>>>
    {
        public GetCoursesByStudentQuery(int studentId)
        {
            StudentId = studentId;
        }

        public int StudentId { get; set; }
    }
}
