using Project.Core.Features.Questions.Queries.Models;
using Project.Core.Features.Questions.Queries.Results;

namespace Project.Core.Features.Questions.Queries.Handlers
{
    public class QuestionQueryHandler : ResponseHandler,
        IRequestHandler<GetAllQuestionsQuery, Response<IEnumerable<QuestionResponse>>>,
        IRequestHandler<GetQuestionByIdQuery, Response<QuestionResponse>>
    {
        private readonly IQuestionService _service;
        private readonly IMapper _mapper;

        public QuestionQueryHandler(IQuestionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<QuestionResponse>>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            var items = await _service.GetAllAsync(cancellationToken);
            var result = items.Select(q => new QuestionResponse
            {
                Id = q.Id,
                QuestionType = q.QuestionType,
                Content = q.Content,
                AnswerType = q.AnswerType,
                Score = q.Score,
                ExamId = q.ExamId,
                CorrectByAssistant = q.CorrectByAssistant,
                Options = q.Options.Select(o => new QuestionOptions.Queries.Results.QuestionOptionResponse
                {
                    Id = o.Id,
                    Content = o.Content,
                    IsCorrect = o.IsCorrect,
                    QuestionId = o.QuestionId

                })
            }).ToList();
            return Success<IEnumerable<QuestionResponse>>(result);
        }

        public async Task<Response<QuestionResponse>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (item is null) return NotFound<QuestionResponse>("Question not found");
            var resp = new QuestionResponse
            {
                Id = item.Id,
                QuestionType = item.QuestionType,
                Content = item.Content,
                AnswerType = item.AnswerType,
                Score = item.Score,
                ExamId = item.ExamId,
                CorrectByAssistant = item.CorrectByAssistant,
                Options = item.Options.Select(o => new QuestionOptions.Queries.Results.QuestionOptionResponse
                {
                    Id = o.Id,
                    Content = o.Content,
                    IsCorrect = o.IsCorrect,
                    QuestionId = o.QuestionId
                })
            };
            return Success(resp);
        }
    }
}
