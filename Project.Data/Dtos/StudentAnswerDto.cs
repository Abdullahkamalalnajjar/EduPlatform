namespace Project.Data.Dtos
{
    public class StudentAnswerDto
    {
        public int QuestionId { get; set; }
        public List<int> SelectedOptionIds { get; set; } = new();
        public string? TextAnswer { get; set; }
        public string? ImageAnswerUrl { get; set; } // For storing image answer URL
    }
}
