using MediatR;
using Project.Core.Features.QuestionOptions.Queries.Models;
using Project.Core.Features.QuestionOptions.Queries.Results;
using Project.Service.Abstracts;

namespace Project.Core.Features.QuestionOptions.Queries.Handlers
{
    public class QuestionOptionQueryHandler : ResponseHandler,
        IRequestHandler<GetAllQuestionOptionsQuery, Response<IEnumerable<QuestionOptionResponse>>>,
        IRequestHandler<GetQuestionOptionByIdQuery, Response<QuestionOptionResponse>>
    {
        private readonly IQuestionOptionService _service;

        public QuestionOptionQueryHandler(IQuestionOptionService service)
        {
            _service = service;
        }

        public async Task<Response<IEnumerable<QuestionOptionResponse>>> Handle(GetAllQuestionOptionsQuery request, CancellationToken cancellationToken)
        {
            var items = await _service.GetAllAsync(cancellationToken);
            var result = items.Select(o => new QuestionOptionResponse { Id = o.Id, Content = o.Content, IsCorrect = o.IsCorrect, QuestionId = o.QuestionId }).ToList();
            return Success<IEnumerable<QuestionOptionResponse>>(result);
        }

        public async Task<Response<QuestionOptionResponse>> Handle(GetQuestionOptionByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (item is null) return NotFound<QuestionOptionResponse>("QuestionOption not found");
            var resp = new QuestionOptionResponse { Id = item.Id, Content = item.Content, IsCorrect = item.IsCorrect, QuestionId = item.QuestionId };
            return Success(resp);
        }
    }
}
