namespace Project.Data.Entities.Exams
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int LectureId { get; set; }
        public Project.Data.Entities.Content.Lecture Lecture { get; set; } = null!;

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}