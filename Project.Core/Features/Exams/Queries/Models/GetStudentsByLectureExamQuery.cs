using MediatR;
using Project.Core.Bases;
using Project.Data.Dtos;

namespace Project.Core.Features.Exams.Queries.Models
{
    public class GetStudentsByLectureExamQuery : IRequest<Response<IEnumerable<StudentExamSubmissionDto>>>
    {
        public int LectureId { get; set; }
    }
}
