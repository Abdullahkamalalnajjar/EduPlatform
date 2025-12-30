using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.StudentExamResults.Commands.Models
{
    public class CreateStudentExamResultCommand : IRequest<Response<int>>
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int TotalScore { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
