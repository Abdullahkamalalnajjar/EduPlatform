namespace Project.Core.Features.Subjects.Queries.Models
{
    public class GetTeacherWithCourseBySubjectIdQuery : IRequest<Response<IEnumerable<SubjectDto>>>
    {
        public int SubjectId { get; set; }
    }
}
