namespace Project.Core.Features.Exams.Queries.Results
{
    public class ExamResponse
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public string LectureTitle { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
    }
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public List<OptionResponse> Options { get; set; } = new();
        public string CorrectAnswer { get; set; } = null!;
    }
}
