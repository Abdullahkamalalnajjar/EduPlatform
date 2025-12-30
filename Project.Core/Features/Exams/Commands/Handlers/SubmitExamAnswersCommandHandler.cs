using Project.Core.Features.Exams.Commands.Models;
using Project.Data.Entities.Exams;

namespace Project.Core.Features.Exams.Commands.Handlers
{
    public class SubmitExamAnswersCommandHandler : ResponseHandler, IRequestHandler<SubmitExamAnswersCommand, Response<int>>
    {
        private readonly IStudentExamResultService _resultService;
        private readonly IUnitOfWork _unitOfWork;

        public SubmitExamAnswersCommandHandler(IStudentExamResultService resultService, IUnitOfWork unitOfWork)
        {
            _resultService = resultService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(SubmitExamAnswersCommand request, CancellationToken cancellationToken)
        {
            // calculate score
            var total = await _resultService.CalculateTotalScoreAsync(request.ExamId, request.Answers, cancellationToken);

            // create result entity
            var result = new StudentExamResult
            {
                StudentId = request.StudentId,
                ExamId = request.ExamId,
                TotalScore = total,
                SubmittedAt = DateTime.UtcNow
            };

            var created = await _resultService.CreateAsync(result, cancellationToken);
            return Success(created.Id);
        }
    }
}
