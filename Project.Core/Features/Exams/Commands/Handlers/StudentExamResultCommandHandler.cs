using AutoMapper;
using MediatR;
using Project.Core.Features.Exams.Commands.Models;
using Project.Service.Abstracts;
using Project.Data.Entities.Exams;

namespace Project.Core.Features.Exams.Commands.Handlers
{
    public class StudentExamResultCommandHandler : ResponseHandler,
        IRequestHandler<CreateStudentExamResultCommand, Response<int>>,
        IRequestHandler<EditStudentExamResultCommand, Response<int>>,
        IRequestHandler<DeleteStudentExamResultCommand, Response<string>>
    {
        private readonly IStudentExamResultService _service;
        private readonly IMapper _mapper;

        public StudentExamResultCommandHandler(IStudentExamResultService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateStudentExamResultCommand request, CancellationToken cancellationToken)
        {
            var entity = new StudentExamResult { StudentId = request.StudentId, ExamId = request.ExamId, TotalScore = request.TotalScore, SubmittedAt = request.SubmittedAt };
            var created = await _service.CreateAsync(entity, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditStudentExamResultCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<int>("StudentExamResult not found");
            entity.StudentId = request.StudentId;
            entity.ExamId = request.ExamId;
            entity.TotalScore = request.TotalScore;
            entity.SubmittedAt = request.SubmittedAt;
            var updated = await _service.UpdateAsync(entity, cancellationToken);
            return Success(updated.Id);
        }

        public async Task<Response<string>> Handle(DeleteStudentExamResultCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<string>("StudentExamResult not found");
            await _service.DeleteAsync(request.Id, cancellationToken);
            return Success("Deleted");
        }
    }
}
