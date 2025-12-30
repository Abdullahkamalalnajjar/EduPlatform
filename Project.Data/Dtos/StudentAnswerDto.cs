namespace Project.Data.Dtos
{
    // Represents an answer from a student: QuestionId and selected OptionIds (for MCQ)
    public class StudentAnswerDto
    {
        public int QuestionId { get; set; }
        public List<int> SelectedOptionIds { get; set; } = new();
        public string? TextAnswer { get; set; }
    }
}
