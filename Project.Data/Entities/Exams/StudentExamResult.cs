namespace Project.Data.Entities.Exams
{
    public class StudentExamResult
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Project.Data.Entities.People.Student Student { get; set; } = null!;
        public bool IsFinashed { get; set; } = false;

        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public int TotalScore { get; set; }

        public DateTime SubmittedAt { get; set; }
    }
}