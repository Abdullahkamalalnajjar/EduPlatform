using MediatR;
using Project.Core.Bases;
using Project.Core.Features.Exams.Queries.Results;

namespace Project.Core.Features.Exams.Queries.Models
{
    public class GetStudentExamScoreQuery : IRequest<Response<GetStudentExamScoreResponse>>
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
    }
}
