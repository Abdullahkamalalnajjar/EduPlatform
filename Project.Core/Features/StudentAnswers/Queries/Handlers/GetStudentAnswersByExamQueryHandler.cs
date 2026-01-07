using MediatR;
using Project.Core.Features.StudentAnswers.Queries.Models;
using Project.Service.Abstracts;

namespace Project.Core.Features.StudentAnswers.Queries.Handlers
{
    public class GetStudentAnswersByExamQueryHandler : ResponseHandler, IRequestHandler<GetStudentAnswersByExamQuery, Response<IEnumerable<StudentAnswerResponse>>>
    {
        private readonly IStudentAnswerService _studentAnswerService;
        private readonly IUnitOfWork _unitOfWork;

        public GetStudentAnswersByExamQueryHandler(IStudentAnswerService studentAnswerService, IUnitOfWork unitOfWork)
        {
            _studentAnswerService = studentAnswerService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<StudentAnswerResponse>>> Handle(GetStudentAnswersByExamQuery request, CancellationToken cancellationToken)
        {
            var answers = await _studentAnswerService.GetByStudentExamResultIdAsync(request.StudentExamResultId, cancellationToken);

            if (!answers.Any())
                return NotFound<IEnumerable<StudentAnswerResponse>>("No answers found for this exam");

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

            var result = answers.Select(sa => new StudentAnswerResponse
            {
                Id = sa.Id,
                QuestionId = sa.QuestionId,
                QuestionContent = sa.Question?.Content ?? "Unknown",
                QuestionType = sa.Question?.QuestionType ?? "Unknown",
                AnswerType = sa.Question?.AnswerType ?? "Unknown",
                SelectedOptionIds = string.Join(",", sa.SelectedOptions.Select(so => so.QuestionOptionId)), // Convert back to comma-separated IDs if needed
                TextAnswer = sa.TextAnswer,
                ImageAnswerUrl = sa.ImageAnswerUrl,
                PointsEarned = sa.PointsEarned,
                IsCorrect = sa.IsCorrect,
                MaxScore = sa.Question?.Score ?? 0,
                Feedback = sa.Feedback, // ??????? ??????
                GradedByName = !string.IsNullOrEmpty(sa.GradedByUserId) && graders.ContainsKey(sa.GradedByUserId) 
                    ? graders[sa.GradedByUserId] 
                    : null, // ??? ??????
                QuestionOptions = sa.Question?.Options?.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Content = o.Content,
                    IsCorrect = o.IsCorrect,
                    IsSelected = sa.SelectedOptions.Any(so => so.QuestionOptionId == o.Id)
                }).ToList() ?? new List<OptionDto>()
            }).ToList();

            return Success<IEnumerable<StudentAnswerResponse>>(result);
        }
    }
}
