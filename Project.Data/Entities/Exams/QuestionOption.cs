namespace Project.Data.Entities.Exams
{
    public class QuestionOption
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!; // ?? ?? ????
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}