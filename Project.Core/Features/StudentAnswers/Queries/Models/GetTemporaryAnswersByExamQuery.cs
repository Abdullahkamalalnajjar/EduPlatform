using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.StudentAnswers.Queries.Models
{
    public class GetTemporaryAnswersByExamQuery : IRequest<Response<IEnumerable<TemporaryAnswerDto>>>
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
    }

    public class TemporaryAnswerDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string QuestionContent { get; set; } = null!;
        public string? ImageAnswerUrl { get; set; }
        public string? TextAnswer { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
