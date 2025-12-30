namespace Project.Core.Features.Exams.Queries.Results
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string QuestionType { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string AnswerType { get; set; } = null!;
        public int Score { get; set; }
        public int ExamId { get; set; }
        public IEnumerable<OptionResponse> Options { get; set; } = new List<OptionResponse>();
    }
    public class OptionResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
