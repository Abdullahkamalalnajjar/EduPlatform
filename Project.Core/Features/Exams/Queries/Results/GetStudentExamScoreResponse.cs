namespace Project.Core.Features.Exams.Queries.Results
{
    public class GetStudentExamScoreResponse
    {
        public int ExamId { get; set; }
        public string ExamTitle { get; set; } = null!;
        public int StudentExamResultId { get; set; }
        public int TotalScore { get; set; }
        public bool IsFinished { get; set; }
        public DateTime SubmittedAt { get; set; }
        public IEnumerable<StudentAnswerSummary> StudentAnswers { get; set; } = new List<StudentAnswerSummary>();
    }

    public class StudentAnswerSummary
    {
        public int QuestionId { get; set; }
        public string QuestionContent { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
        public string AnswerType { get; set; } = null!;
        public int MaxScore { get; set; }
        public int? PointsEarned { get; set; }
        public bool IsCorrect { get; set; }
        public string? TextAnswer { get; set; }
        public string? ImageAnswerUrl { get; set; }
        public IEnumerable<SelectedOptionSummary> SelectedOptions { get; set; } = new List<SelectedOptionSummary>();
        public IEnumerable<QuestionOptionSummary> QuestionOptions { get; set; } = new List<QuestionOptionSummary>();
    }

    public class SelectedOptionSummary
    {
        public int OptionId { get; set; }
        public string OptionContent { get; set; } = null!;
        public bool IsCorrect { get; set; }
    }

    public class QuestionOptionSummary
    {
        public int OptionId { get; set; }
        public string OptionContent { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public bool IsSelected { get; set; }
    }
}
