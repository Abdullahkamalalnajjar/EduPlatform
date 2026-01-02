using Project.Core.Features.Exams.Queries.Results;

namespace Project.Core.Features.Exams.Queries.Models
{
    public class GetExamByLectureIdQuery : IRequest<Response<ExamResponse>>
    {
        public int LectureId { get; set; }
    }
}
