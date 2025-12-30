using MediatR;
using Project.Core.Bases;
using Project.Data.Dtos;

namespace Project.Core.Features.Exams.Commands.Models
{
    public class SubmitExamAnswersCommand : IRequest<Response<int>>
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public List<StudentAnswerDto> Answers { get; set; } = new List<StudentAnswerDto>();
    }
}
