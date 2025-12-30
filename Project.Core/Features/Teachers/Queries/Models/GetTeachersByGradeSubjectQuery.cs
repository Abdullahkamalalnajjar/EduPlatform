using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Teachers.Queries.Models
{
    public class GetTeachersByGradeSubjectQuery : IRequest<Response<IEnumerable<Project.Core.Features.Teachers.Queries.Results.TeacherByGradeSubjectResponse>>>
    {
        public int GradeYear { get; set; }
        public int SubjectId { get; set; }
    }
}
