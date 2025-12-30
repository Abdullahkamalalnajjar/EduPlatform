using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Queries.Models
{
    public class GetStudentExamScoreQuery : IRequest<Response<int>>
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
    }
}
