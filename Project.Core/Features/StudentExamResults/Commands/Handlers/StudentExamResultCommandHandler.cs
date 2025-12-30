using AutoMapper;
using MediatR;
using Project.Core.Features.StudentExamResults.Commands.Models;
using Project.Service.Abstracts;
using Project.Data.Entities.Exams;

namespace Project.Core.Features.StudentExamResults.Commands.Handlers
{
    public class StudentExamResultCommandHandler : ResponseHandler,
        IRequestHandler<CreateStudentExamResultCommand, Response<int>>
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
    }
}
