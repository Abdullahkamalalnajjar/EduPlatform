using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.StudentAnswers.Queries.Models
{
    public class GetStudentAnswersByExamQuery : IRequest<Response<IEnumerable<StudentAnswerResponse>>>
    {
        public int StudentExamResultId { get; set; }
    }

    public class StudentAnswerResponse
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string QuestionContent { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
        public string AnswerType { get; set; } = null!;
        public string? SelectedOptionIds { get; set; }
        public string? TextAnswer { get; set; }
        public string? ImageAnswerUrl { get; set; }
        public decimal? PointsEarned { get; set; } // ???? ??????? ???????
        public bool IsCorrect { get; set; }
        public int MaxScore { get; set; }
        public string? Feedback { get; set; } // ??????? ??????
        public string? GradedByName { get; set; } // ??? ??????
        public IEnumerable<OptionDto> QuestionOptions { get; set; } = new List<OptionDto>();
    }

    public class OptionDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public bool IsSelected { get; set; }
    }
}
