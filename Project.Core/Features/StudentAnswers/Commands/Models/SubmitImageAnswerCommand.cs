namespace Project.Core.Features.StudentAnswers.Commands.Models
{
    public class SubmitImageAnswerCommand : IRequest<Response<int>>
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        public int StudentExamResultId { get; set; } // 0 if not submitted yet
        public IFormFile ImageFile { get; set; } = null!;
    }
}
