using Project.Core.Features.Exams.Commands.Models;
using Project.Data.Entities.Exams;

namespace Project.Core.Features.Exams.Commands.Handlers
{
    public class SubmitExamAnswersCommandHandler : ResponseHandler, IRequestHandler<SubmitExamAnswersCommand, Response<int>>
    {
        private readonly IStudentExamResultService _resultService;
        private readonly IExamService _examService;
        private readonly IUnitOfWork _unitOfWork;

        public SubmitExamAnswersCommandHandler(IStudentExamResultService resultService, IExamService examService, IUnitOfWork unitOfWork)
        {
            _resultService = resultService;
            _examService = examService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(SubmitExamAnswersCommand request, CancellationToken cancellationToken)
        {
            // Get the exam and mark it as finished
            var exam = await _examService.GetByIdAsync(request.ExamId, cancellationToken);
            if (exam is null) return NotFound<int>("Exam not found");

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

            // Mark exam as finished
            exam.IsFinashed = true;
            await _examService.UpdateAsync(exam, cancellationToken);

            return Success(created.Id);
        }
    }
}
