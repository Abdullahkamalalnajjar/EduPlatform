namespace Project.Data.Dtos
{
    public class StudentExamSubmissionDto
    {
        public int StudentExamResultId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public string StudentEmail { get; set; } = null!;
        public int ExamId { get; set; }
        public string ExamTitle { get; set; } = null!;
        public int CurrentTotalScore { get; set; }
        public int MaxScore { get; set; }
        public bool IsFinished { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int TotalAnswers { get; set; }
        public int ManuallyGradedAnswers { get; set; }
        public int PendingGradingAnswers { get; set; }
    }
}
