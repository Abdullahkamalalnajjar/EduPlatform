using AutoMapper;
using MediatR;
using Project.Core.Features.Exams.Queries.Models;
using Project.Core.Features.Exams.Queries.Results;
using Project.Service.Abstracts;

namespace Project.Core.Features.Exams.Queries.Handlers
{
    public class StudentExamResultQueryHandler : ResponseHandler,
        IRequestHandler<GetAllStudentExamResultsQuery, Response<IEnumerable<StudentExamResultResponse>>>,
        IRequestHandler<GetStudentExamResultByIdQuery, Response<StudentExamResultResponse>>
    {
        private readonly IStudentExamResultService _service;
        private readonly IMapper _mapper;

        public StudentExamResultQueryHandler(IStudentExamResultService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<StudentExamResultResponse>>> Handle(GetAllStudentExamResultsQuery request, CancellationToken cancellationToken)
        {
            var items = await _service.GetAllAsync(cancellationToken);
            var result = items.Select(r => new StudentExamResultResponse { Id = r.Id, StudentId = r.StudentId, ExamId = r.ExamId, TotalScore = r.TotalScore, SubmittedAt = r.SubmittedAt }).ToList();
            return Success<IEnumerable<StudentExamResultResponse>>(result);
        }

        public async Task<Response<StudentExamResultResponse>> Handle(GetStudentExamResultByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (item is null) return NotFound<StudentExamResultResponse>("StudentExamResult not found");
            var resp = new StudentExamResultResponse { Id = item.Id, StudentId = item.StudentId, ExamId = item.ExamId, TotalScore = item.TotalScore, SubmittedAt = item.SubmittedAt };
            return Success(resp);
        }
    }
}
