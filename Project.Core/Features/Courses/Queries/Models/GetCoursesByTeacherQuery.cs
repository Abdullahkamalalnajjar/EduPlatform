namespace Project.Core.Features.Courses.Queries.Models
{
    public class GetCoursesByTeacherQuery : IRequest<Response<IEnumerable<CourseDto>>>
    {
        public int TeacherId { get; set; }
    }
}
