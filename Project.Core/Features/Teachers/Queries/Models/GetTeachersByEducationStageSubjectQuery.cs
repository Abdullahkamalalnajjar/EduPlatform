using Project.Core.Features.Teachers.Queries.Results;

namespace Project.Core.Features.Teachers.Queries.Models
{
    public class GetTeachersByEducationStageSubjectQuery : IRequest<Response<IEnumerable<TeacherByGradeSubjectResponse>>>
    {
        public int EducationStageId { get; set; }
        public int SubjectId { get; set; }
    }
}
