using MediatR;
using Project.Core.Features.QuestionOptions.Commands.Models;
using Project.Service.Abstracts;
using Project.Data.Entities.Exams;

namespace Project.Core.Features.QuestionOptions.Commands.Handlers
{
    public class QuestionOptionCommandHandler : ResponseHandler,
        IRequestHandler<CreateQuestionOptionCommand, Response<int>>,
        IRequestHandler<EditQuestionOptionCommand, Response<int>>,
        IRequestHandler<DeleteQuestionOptionCommand, Response<string>>
    {
        private readonly IQuestionOptionService _service;

        public QuestionOptionCommandHandler(IQuestionOptionService service)
        {
            _service = service;
        }

        public async Task<Response<int>> Handle(CreateQuestionOptionCommand request, CancellationToken cancellationToken)
        {
            var entity = new QuestionOption { Content = request.Content, IsCorrect = request.IsCorrect, QuestionId = request.QuestionId };
            var created = await _service.CreateAsync(entity, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditQuestionOptionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<int>("QuestionOption not found");
            entity.Content = request.Content;
            entity.IsCorrect = request.IsCorrect;
            entity.QuestionId = request.QuestionId;
            var updated = await _service.UpdateAsync(entity, cancellationToken);
            return Success(updated.Id);
        }

        public async Task<Response<string>> Handle(DeleteQuestionOptionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<string>("QuestionOption not found");
            await _service.DeleteAsync(request.Id, cancellationToken);
            return Success("Deleted");
        }
    }
}
