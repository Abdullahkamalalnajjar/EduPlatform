using Project.Core.Features.Exams.Queries.Models;
using Project.Core.Features.Exams.Queries.Results;

namespace Project.Core.Features.Exams.Queries.Handlers
{
    public class GetStudentExamScoreQueryHandler : ResponseHandler, IRequestHandler<GetStudentExamScoreQuery, Response<GetStudentExamScoreResponse>>
    {
        private readonly IStudentExamResultService _resultService;
        private readonly IStudentAnswerService _answerService;

        public GetStudentExamScoreQueryHandler(IStudentExamResultService resultService, IStudentAnswerService answerService)
        {
            _resultService = resultService;
            _answerService = answerService;
        }

        public async Task<Response<GetStudentExamScoreResponse>> Handle(GetStudentExamScoreQuery request, CancellationToken cancellationToken)
        {
            var result = await _resultService.GetAllAsync(cancellationToken);
            var record = result.FirstOrDefault(r => r.ExamId == request.ExamId && r.StudentId == request.StudentId);
            if (record is null) return NotFound<GetStudentExamScoreResponse>("Result not found");

            // Fetch student answers for this exam result
            var answers = await _answerService.GetByStudentExamResultIdAsync(record.Id, cancellationToken);

            // Map to response
            var response = new GetStudentExamScoreResponse
            {
                ExamId = record.ExamId,
                ExamTitle = record.Exam?.Title ?? "Unknown",
                StudentExamResultId = record.Id,
                TotalScore = record.TotalScore,
                IsFinished = record.IsFinashed,
                SubmittedAt = record.SubmittedAt,
                StudentAnswers = answers.Select(a => new StudentAnswerSummary
                {
                    QuestionId = a.QuestionId,
                    QuestionContent = a.Question?.Content ?? "Unknown",
                    QuestionType = a.Question?.QuestionType ?? "Unknown",
                    AnswerType = a.Question?.AnswerType ?? "Unknown",
                    MaxScore = a.Question?.Score ?? 0,
                    PointsEarned = a.PointsEarned,
                    IsCorrect = a.IsCorrect,
                    TextAnswer = a.TextAnswer,
                    ImageAnswerUrl = a.ImageAnswerUrl,
                    SelectedOptions = a.SelectedOptions.Select(so => new SelectedOptionSummary
                    {
                        OptionId = so.QuestionOptionId,
                        OptionContent = so.QuestionOption?.Content ?? "Unknown",
                        IsCorrect = so.QuestionOption?.IsCorrect ?? false
                    }).ToList(),
                    QuestionOptions = a.Question?.Options?.Select(o => new QuestionOptionSummary
                    {
                        OptionId = o.Id,
                        OptionContent = o.Content,
                        IsCorrect = o.IsCorrect,
                        IsSelected = a.SelectedOptions.Any(so => so.QuestionOptionId == o.Id)
                    }).ToList() ?? new List<QuestionOptionSummary>()
                }).ToList()
            };

            return Success(response);
        }
    }
}
