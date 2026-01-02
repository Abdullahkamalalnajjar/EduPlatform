using AutoMapper;
using MediatR;
using Project.Core.Features.Exams.Commands.Models;
using Project.Service.Abstracts;
using Project.Data.Entities.Exams;

namespace Project.Core.Features.Exams.Commands.Handlers
{
    public class ExamCommandHandler : ResponseHandler,
        IRequestHandler<CreateExamCommand, Response<int>>,
        IRequestHandler<EditExamCommand, Response<int>>,
        IRequestHandler<DeleteExamCommand, Response<string>>
    {
        private readonly IExamService _service;
        private readonly IMapper _mapper;
        private readonly ILectureService _lectureService;

        public ExamCommandHandler(IExamService service, IMapper mapper, ILectureService lectureService)
        {
            _service = service;
            _mapper = mapper;
            _lectureService = lectureService;
        }

        public async Task<Response<int>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            var lecture = await _lectureService.GetByIdAsync(request.LectureId, cancellationToken);
            if (lecture is null) return NotFound<int>("Lecture not found");

            var entity = new Exam { LectureId = request.LectureId };
            var created = await _service.CreateAsync(entity, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditExamCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<int>("Exam not found");

            var lecture = await _lectureService.GetByIdAsync(request.LectureId, cancellationToken);
            if (lecture is null) return NotFound<int>("Lecture not found");

            entity.LectureId = request.LectureId;
            var updated = await _service.UpdateAsync(entity, cancellationToken);
            return Success(updated.Id);
        }

        public async Task<Response<string>> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<string>("Exam not found");
            await _service.DeleteAsync(request.Id, cancellationToken);
            return Success("Deleted");
        }
    }
}
