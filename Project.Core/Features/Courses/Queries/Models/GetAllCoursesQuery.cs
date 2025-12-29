using MediatR;
using Project.Core.Features.Courses.Queries.Results;

namespace Project.Core.Features.Courses.Queries.Models
{
    public class GetAllCoursesQuery : IRequest<Response<IEnumerable<CourseResponse>>>
    {
    }
}