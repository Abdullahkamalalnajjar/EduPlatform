namespace Project.Core.Features.StudentExamResults.Queries.Results
{
    public class StudentExamResultResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int TotalScore { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsFinished { get; set; }
    }
}
