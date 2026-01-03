using Project.Core.Features.Exams.Commands.Models;
using Project.Data.Entities.Exams;
using System.Text.Json;

namespace Project.Core.Features.Exams.Commands.Handlers
{
    public class SubmitExamAnswersCommandHandler : ResponseHandler, IRequestHandler<SubmitExamAnswersCommand, Response<int>>
    {
        private readonly IStudentExamResultService _resultService;
        private readonly IStudentAnswerService _answerService;
        private readonly IExamService _examService;
        private readonly IUnitOfWork _unitOfWork;

        public SubmitExamAnswersCommandHandler(IStudentExamResultService resultService, IStudentAnswerService answerService, IExamService examService, IUnitOfWork unitOfWork)
        {
            _resultService = resultService;
            _answerService = answerService;
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

            // Save individual student answers
            var studentAnswers = new List<StudentAnswer>();
            foreach (var answer in request.Answers)
            {
                var studentAnswer = new StudentAnswer
                {
                    StudentExamResultId = created.Id,
                    QuestionId = answer.QuestionId,
                    SelectedOptionIds = answer.SelectedOptionIds != null && answer.SelectedOptionIds.Any() 
                        ? JsonSerializer.Serialize(answer.SelectedOptionIds)
                        : null,
                    TextAnswer = answer.TextAnswer,
                    IsCorrect = false // Will be calculated based on answer type
                };
                studentAnswers.Add(studentAnswer);
            }

            // Bulk create all student answers
            if (studentAnswers.Any())
            {
                await _answerService.CreateBulkAsync(studentAnswers, cancellationToken);
            }

            // Mark exam as finished
            exam.IsFinashed = true;
            await _examService.UpdateAsync(exam, cancellationToken);

            return Success(created.Id);
        }
    }
}
