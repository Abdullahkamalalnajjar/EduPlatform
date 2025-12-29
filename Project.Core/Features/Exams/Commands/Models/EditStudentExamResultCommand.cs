using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Commands.Models
{
    public class EditStudentExamResultCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int TotalScore { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
