namespace Project.Data.Entities.Exams
{
    public class StudentAnswerOption
    {
        public int Id { get; set; }

        public int StudentAnswerId { get; set; }
        public StudentAnswer StudentAnswer { get; set; } = null!;

        public int QuestionOptionId { get; set; }
        public QuestionOption QuestionOption { get; set; } = null!;
    }
}
