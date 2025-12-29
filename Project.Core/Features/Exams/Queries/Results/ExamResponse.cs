namespace Project.Core.Features.Exams.Queries.Results
{
    public class ExamResponse
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public IEnumerable<int> QuestionIds { get; set; } = new List<int>();
    }
}
