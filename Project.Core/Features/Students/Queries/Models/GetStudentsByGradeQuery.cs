using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Students.Queries.Models
{
    public class GetStudentsByGradeQuery : IRequest<Response<IEnumerable<Project.Core.Features.Students.Queries.Results.StudentByGradeResponse>>>
    {
        public int GradeYear { get; set; }
    }
}
