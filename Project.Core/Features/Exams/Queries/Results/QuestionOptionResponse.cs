namespace Project.Core.Features.Exams.Queries.Results
{
    public class QuestionOptionResponse
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
