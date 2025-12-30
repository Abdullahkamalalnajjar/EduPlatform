using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Teachers.Queries.Models
{
    public class GetTeacherEducationStagesQuery : IRequest<Response<IEnumerable<Project.Core.Features.Teachers.Queries.Results.TeacherByGradeSubjectResponse>>>
    {
        public int TeacherId { get; set; }
    }
}
