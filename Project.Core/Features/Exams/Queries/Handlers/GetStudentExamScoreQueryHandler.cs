using Project.Core.Features.Exams.Queries.Models;
using Project.Core.Features.Exams.Queries.Results;

namespace Project.Core.Features.Exams.Queries.Handlers
{
    public class GetStudentExamScoreQueryHandler : ResponseHandler, IRequestHandler<GetStudentExamScoreQuery, Response<GetStudentExamScoreResponse>>
    {
        private readonly IStudentExamResultService _resultService;
        private readonly IStudentAnswerService _answerService;
        private readonly IUnitOfWork _unitOfWork;

        public GetStudentExamScoreQueryHandler(IStudentExamResultService resultService, IStudentAnswerService answerService, IUnitOfWork unitOfWork)
        {
            _resultService = resultService;
            _answerService = answerService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetStudentExamScoreResponse>> Handle(GetStudentExamScoreQuery request, CancellationToken cancellationToken)
        {
            var result = await _resultService.GetAllAsync(cancellationToken);
            var record = result.FirstOrDefault(r => r.ExamId == request.ExamId && r.StudentId == request.StudentId);
            if (record is null) return NotFound<GetStudentExamScoreResponse>("Result not found");

            // Fetch student answers for this exam result
            var answers = await _answerService.GetByStudentExamResultIdAsync(record.Id, cancellationToken);

            // Create a dictionary of grader names for performance
            var graderIds = answers.Where(a => !string.IsNullOrEmpty(a.GradedByUserId)).Select(a => a.GradedByUserId).Distinct().ToList();
            var graders = new Dictionary<string, string>();
            
            if (graderIds.Any())
            {
                var graderUsers = await _unitOfWork.Users.GetTableNoTracking()
                    .Where(u => graderIds.Contains(u.Id))
                    .Select(u => new { u.Id, u.FirstName, u.LastName })
                    .ToListAsync(cancellationToken);

                graders = graderUsers.ToDictionary(
                    g => g.Id,
                    g => $"{g.FirstName} {g.LastName}".Trim()
                );
            }

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
                    StudentAnswerId = a.Id,
                    QuestionId = a.QuestionId,
                    QuestionContent = a.Question?.Content ?? "Unknown",
                    QuestionType = a.Question?.QuestionType ?? "Unknown",
                    AnswerType = a.Question?.AnswerType ?? "Unknown",
                    CorrectByAssistant = a.Question.CorrectByAssistant,
                    MaxScore = a.Question?.Score ?? 0,
                    PointsEarned = a.PointsEarned,
                    IsCorrect = a.IsCorrect,
                    TextAnswer = a.TextAnswer,
                    ImageAnswerUrl = a.ImageAnswerUrl,
                    Feedback = a.Feedback, // ????? ??????? ??????
                    GradedByName = !string.IsNullOrEmpty(a.GradedByUserId) && graders.ContainsKey(a.GradedByUserId) 
                        ? graders[a.GradedByUserId] 
                        : null, // ??? ??????
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
