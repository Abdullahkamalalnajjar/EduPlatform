using Project.Core.Features.Teachers.Queries.Results;

namespace Project.Core.Features.Teachers.Queries.Models
{
    public class GetTeacherCourseRevenueQuery : IRequest<Response<TeacherCourseRevenueResponse>>
    {
        public int TeacherId { get; set; }
    }
}
