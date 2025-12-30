using Project.Core.Features.Questions.Queries.Results;

namespace Project.Core.Features.Exams.Queries.Results
{
    public class ExamResponse
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public string LectureName { get; set; }
        public IEnumerable<QuestionResponse> Questions { get; set; } = new List<QuestionResponse>();
    }


}
