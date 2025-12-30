namespace Project.Core.Features.QuestionOptions.Queries.Results
{
    public class QuestionOptionResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
