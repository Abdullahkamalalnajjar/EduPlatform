namespace Project.Core.Features.Exams.Commands.Models
{
    public class GradeExamCommand : IRequest<Response<GradeExamResponse>>
    {
        public int StudentExamResultId { get; set; }
        public List<GradeAnswerDto> GradedAnswers { get; set; } = new();
    }

    public class GradeAnswerDto
    {
        public int StudentAnswerId { get; set; }
        public int PointsEarned { get; set; }
        public bool IsCorrect { get; set; }
        public string? Feedback { get; set; }
    }

    public class GradeExamResponse
    {
        public int StudentExamResultId { get; set; }
        public int PreviousTotalScore { get; set; }
        public int NewTotalScore { get; set; }
        public int PointsFromManualGrading { get; set; }
        public string Message { get; set; } = null!;
    }
}
