using AutoMapper;
using MediatR;
using Project.Core.Features.Exams.Commands.Models;
using Project.Service.Abstracts;
using Project.Data.Entities.Exams;
using Microsoft.AspNetCore.Http;

namespace Project.Core.Features.Exams.Commands.Handlers
{
    public class QuestionCommandHandler : ResponseHandler,
        IRequestHandler<CreateQuestionCommand, Response<int>>,
        IRequestHandler<EditQuestionCommand, Response<int>>,
        IRequestHandler<DeleteQuestionCommand, Response<string>>
    {
        private readonly IQuestionService _service;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public QuestionCommandHandler(IQuestionService service, IFileService fileService, IMapper mapper)
        {
            _service = service;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            // If the question is an image, upload the file and set Content to the returned URL
            if (string.Equals(request.QuestionType, "Image", StringComparison.OrdinalIgnoreCase) && request.File != null)
            {
                var url = await _fileService.UploadImage("questions", request.File);
                if (string.IsNullOrWhiteSpace(url) || url == "FailedToUploadImage" || url == "NoImage")
                {
                    return BadRequest<int>("Failed to upload image");
                }
                request.Content = url;
            }

            var entity = new Question { QuestionType = request.QuestionType, Content = request.Content, AnswerType = request.AnswerType, Score = request.Score, ExamId = request.ExamId };
            var created = await _service.CreateAsync(entity, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditQuestionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<int>("Question not found");

            if (string.Equals(request.QuestionType, "Image", StringComparison.OrdinalIgnoreCase) && request.File != null)
            {
                var url = await _fileService.UploadImage("questions", request.File);
                if (string.IsNullOrWhiteSpace(url) || url == "FailedToUploadImage" || url == "NoImage")
                {
                    return BadRequest<int>("Failed to upload image");
                }
                request.Content = url;
            }

            entity.QuestionType = request.QuestionType;
            entity.Content = request.Content;
            entity.AnswerType = request.AnswerType;
            entity.Score = request.Score;
            entity.ExamId = request.ExamId;
            var updated = await _service.UpdateAsync(entity, cancellationToken);
            return Success(updated.Id);
        }

        public async Task<Response<string>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<string>("Question not found");
            await _service.DeleteAsync(request.Id, cancellationToken);
            return Success("Deleted");
        }
    }
}
