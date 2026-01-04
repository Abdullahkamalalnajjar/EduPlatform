namespace Project.Data.Entities.Exams
{
    public class StudentAnswer
    {
        public int Id { get; set; }

        public int StudentExamResultId { get; set; }
        public StudentExamResult StudentExamResult { get; set; } = null!;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public string? TextAnswer { get; set; } // For text answers
        public string? ImageAnswerUrl { get; set; } // For image answers
        
        public int? PointsEarned { get; set; } // Points earned for this question
        public bool IsCorrect { get; set; } = false; // Whether the answer is correct

        // Navigation property for selected options (replaces JSON SelectedOptionIds)
        public ICollection<StudentAnswerOption> SelectedOptions { get; set; } = new List<StudentAnswerOption>();
    }
}
