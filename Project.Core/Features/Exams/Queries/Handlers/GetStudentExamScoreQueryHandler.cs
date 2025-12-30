using Project.Core.Features.Exams.Queries.Models;

namespace Project.Core.Features.Exams.Queries.Handlers
{
    public class GetStudentExamScoreQueryHandler : ResponseHandler, IRequestHandler<GetStudentExamScoreQuery, Response<int>>
    {
        private readonly IStudentExamResultService _resultService;

        public GetStudentExamScoreQueryHandler(IStudentExamResultService resultService)
        {
            _resultService = resultService;
        }

        public async Task<Response<int>> Handle(GetStudentExamScoreQuery request, CancellationToken cancellationToken)
        {
            var result = await _resultService.GetAllAsync(cancellationToken);
            var record = result.FirstOrDefault(r => r.ExamId == request.ExamId && r.StudentId == request.StudentId);
            if (record is null) return NotFound<int>("Result not found");
            return Success(record.TotalScore);
        }
    }
}
