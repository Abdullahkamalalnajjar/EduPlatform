using Project.Core.Features.QuestionOptions.Queries.Results;

namespace Project.Core.Features.Questions.Queries.Results
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string QuestionType { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string AnswerType { get; set; } = null!;
        public int Score { get; set; }
        public bool CorrectByAssistant { get; set; }
        public int ExamId { get; set; }
        public IEnumerable<QuestionOptionResponse> Options { get; set; } = new List<QuestionOptionResponse>();
    }
}
