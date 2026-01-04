using MediatR;
using Project.Core.Features.StudentAnswers.Queries.Models;
using Project.Service.Abstracts;

namespace Project.Core.Features.StudentAnswers.Queries.Handlers
{
    public class GetTemporaryAnswersByExamQueryHandler : ResponseHandler, IRequestHandler<GetTemporaryAnswersByExamQuery, Response<IEnumerable<TemporaryAnswerDto>>>
    {
        private readonly ITemporaryStudentAnswerService _tempAnswerService;

        public GetTemporaryAnswersByExamQueryHandler(ITemporaryStudentAnswerService tempAnswerService)
        {
            _tempAnswerService = tempAnswerService;
        }

        public async Task<Response<IEnumerable<TemporaryAnswerDto>>> Handle(GetTemporaryAnswersByExamQuery request, CancellationToken cancellationToken)
        {
            var tempAnswers = await _tempAnswerService.GetByStudentAndExamAsync(request.StudentId, request.ExamId, cancellationToken);

            var result = tempAnswers.Select(ta => new TemporaryAnswerDto
            {
                Id = ta.Id,
                QuestionId = ta.QuestionId,
                QuestionContent = ta.Question?.Content ?? "Unknown",
                ImageAnswerUrl = ta.ImageAnswerUrl,
                TextAnswer = ta.TextAnswer,
                CreatedAt = ta.CreatedAt
            }).ToList();

            return Success<IEnumerable<TemporaryAnswerDto>>(result);
        }
    }
}
