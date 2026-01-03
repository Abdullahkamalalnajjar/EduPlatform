using MediatR;
using Project.Core.Features.StudentAnswers.Queries.Models;
using Project.Service.Abstracts;
using System.Text.Json;

namespace Project.Core.Features.StudentAnswers.Queries.Handlers
{
    public class GetStudentAnswersByExamQueryHandler : ResponseHandler, IRequestHandler<GetStudentAnswersByExamQuery, Response<IEnumerable<StudentAnswerResponse>>>
    {
        private readonly IStudentAnswerService _studentAnswerService;

        public GetStudentAnswersByExamQueryHandler(IStudentAnswerService studentAnswerService)
        {
            _studentAnswerService = studentAnswerService;
        }

        public async Task<Response<IEnumerable<StudentAnswerResponse>>> Handle(GetStudentAnswersByExamQuery request, CancellationToken cancellationToken)
        {
            var answers = await _studentAnswerService.GetByStudentExamResultIdAsync(request.StudentExamResultId, cancellationToken);

            if (!answers.Any())
                return NotFound<IEnumerable<StudentAnswerResponse>>("No answers found for this exam");

            var result = answers.Select(sa => new StudentAnswerResponse
            {
                Id = sa.Id,
                QuestionId = sa.QuestionId,
                QuestionContent = sa.Question?.Content ?? "Unknown",
                QuestionType = sa.Question?.QuestionType ?? "Unknown",
                AnswerType = sa.Question?.AnswerType ?? "Unknown",
                SelectedOptionIds = sa.SelectedOptionIds,
                TextAnswer = sa.TextAnswer,
                ImageAnswerUrl = sa.ImageAnswerUrl,
                PointsEarned = sa.PointsEarned,
                IsCorrect = sa.IsCorrect,
                MaxScore = sa.Question?.Score ?? 0,
                QuestionOptions = sa.Question?.Options?.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Content = o.Content,
                    IsCorrect = o.IsCorrect,
                    IsSelected = sa.SelectedOptionIds != null && 
                                JsonSerializer.Deserialize<List<int>>(sa.SelectedOptionIds)?.Contains(o.Id) == true
                }).ToList() ?? new List<OptionDto>()
            }).ToList();

            return Success<IEnumerable<StudentAnswerResponse>>(result);
        }
    }
}
