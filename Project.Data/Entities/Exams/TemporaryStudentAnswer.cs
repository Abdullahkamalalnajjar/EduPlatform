namespace Project.Data.Entities.Exams
{
    public class TemporaryStudentAnswer
    {
        public int Id { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public int StudentId { get; set; }
        public Project.Data.Entities.People.Student Student { get; set; } = null!;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public string? TextAnswer { get; set; } // For text answers
        public string? ImageAnswerUrl { get; set; } // For image answers
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
