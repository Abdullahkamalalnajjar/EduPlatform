namespace Project.Data.Dtos
{
    public class ParentStudentDto
    {
        public int StudentId { get; set; }
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int GradeYear { get; set; }
    }

    public class StudentCourseExamDto
    {
        public int ExamId { get; set; }
        public string ExamTitle { get; set; } = null!;
        public int TotalScore { get; set; }
        public int MaxScore { get; set; }
        public bool IsFinished { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public decimal Percentage { get; set; }
    }
}
