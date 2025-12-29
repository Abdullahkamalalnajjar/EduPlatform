using MediatR;
using Project.Core.Features.Courses.Queries.Results;

namespace Project.Core.Features.Courses.Queries.Models
{
    public class GetCourseByIdQuery : IRequest<Response<CourseResponse>>
    {
        public int Id { get; set; }
    }
}