namespace Project.Data.Entities.Exams
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionType { get; set; } = null!; // Text / Image
        public string Content { get; set; } = null!;       // نص السؤال أو رابط صورة
        public string AnswerType { get; set; } = null!;   // MCQ / ImageAnswer / TextAnswer
        public int Score { get; set; }                     // الدرجة الخاصة بالسؤال
        public bool CorrectByAssistant { get; set; } = false; // هل يتم تصحيح السؤال بمساعدة  
        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
    }
}