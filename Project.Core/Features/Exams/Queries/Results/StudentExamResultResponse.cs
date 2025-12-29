namespace Project.Core.Features.Exams.Queries.Results
{
    public class StudentExamResultResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int TotalScore { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
