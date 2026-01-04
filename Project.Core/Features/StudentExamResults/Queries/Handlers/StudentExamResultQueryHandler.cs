using AutoMapper;
using MediatR;
using Project.Core.Features.StudentExamResults.Queries.Models;
using Project.Core.Features.StudentExamResults.Queries.Results;
using Project.Service.Abstracts;

namespace Project.Core.Features.StudentExamResults.Queries.Handlers
{
    public class StudentExamResultQueryHandler : ResponseHandler,
        IRequestHandler<GetAllStudentExamResultsQuery, Response<IEnumerable<StudentExamResultResponse>>>
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
            var result = items.Select(r => new StudentExamResultResponse
            {
                Id = r.Id,
                StudentId = r.StudentId,
                ExamId = r.ExamId,
                TotalScore = r.TotalScore,
                SubmittedAt = r.SubmittedAt,
                IsFinished = r.IsFinashed
            }).ToList();
            return Success<IEnumerable<StudentExamResultResponse>>(result);
        }
    }
}
