using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Teachers.Queries.Models
{
    public class GetTeachersByEducationStageSubjectQuery : IRequest<Response<IEnumerable<Project.Core.Features.Teachers.Queries.Results.TeacherByGradeSubjectResponse>>>
    {
        public int EducationStageId { get; set; }
        public int SubjectId { get; set; }
    }
}
