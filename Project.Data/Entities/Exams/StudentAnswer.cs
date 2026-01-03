namespace Project.Data.Entities.Exams
{
    public class StudentAnswer
    {
        public int Id { get; set; }

        public int StudentExamResultId { get; set; }
        public StudentExamResult StudentExamResult { get; set; } = null!;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public string? SelectedOptionIds { get; set; } // JSON string of selected option IDs for MCQ
        public string? TextAnswer { get; set; } // For text answers
        public string? ImageAnswerUrl { get; set; } // For image answers
        
        public int? PointsEarned { get; set; } // Points earned for this question
        public bool IsCorrect { get; set; } = false; // Whether the answer is correct
    }
}
