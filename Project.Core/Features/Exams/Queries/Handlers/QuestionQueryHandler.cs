using Project.Core.Features.Exams.Queries.Models;
using Project.Core.Features.Exams.Queries.Results;

namespace Project.Core.Features.Exams.Queries.Handlers
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
            var result = items.Select(q => new QuestionResponse { Id = q.Id, QuestionType = q.QuestionType, Content = q.Content, AnswerType = q.AnswerType, Score = q.Score, ExamId = q.ExamId, OptionIds = q.Options.Select(o => o.Id) }).ToList();
            return Success<IEnumerable<QuestionResponse>>(result);
        }

        public async Task<Response<QuestionResponse>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (item is null) return NotFound<QuestionResponse>("Question not found");
            var resp = new QuestionResponse { Id = item.Id, QuestionType = item.QuestionType, Content = item.Content, AnswerType = item.AnswerType, Score = item.Score, ExamId = item.ExamId, OptionIds = item.Options.Select(o => o.Id) };
            return Success(resp);
        }
    }
}
