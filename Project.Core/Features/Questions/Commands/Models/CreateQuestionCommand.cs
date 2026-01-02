namespace Project.Core.Features.Questions.Commands.Models
{
    public class CreateQuestionCommand : IRequest<Response<int>>
    {
        public string QuestionType { get; set; } = null!;
        // For text questions use Content. For image questions, Content will hold the uploaded file URL and File will be used to upload.
        public string Content { get; set; } = null!;
        public IFormFile? File { get; set; }
        public string AnswerType { get; set; } = null!;
        public int Score { get; set; }
        public bool CorrectByAssistant { get; set; } = false;
        public int ExamId { get; set; }
    }

}
