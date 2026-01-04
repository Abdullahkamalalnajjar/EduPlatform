using Project.Core.Features.Exams.Commands.Models;
using Project.Data.Entities.Exams;
using Project.Service.Abstracts;

namespace Project.Core.Features.Exams.Commands.Handlers
{
    public class SubmitExamAnswersCommandHandler : ResponseHandler, IRequestHandler<SubmitExamAnswersCommand, Response<int>>
    {
        private readonly IStudentExamResultService _resultService;
        private readonly IStudentAnswerService _answerService;
        private readonly ITemporaryStudentAnswerService _tempAnswerService;
        private readonly IUnitOfWork _unitOfWork;

        public SubmitExamAnswersCommandHandler(IStudentExamResultService resultService, IStudentAnswerService answerService, ITemporaryStudentAnswerService tempAnswerService, IUnitOfWork unitOfWork)
        {
            _resultService = resultService;
            _answerService = answerService;
            _tempAnswerService = tempAnswerService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(SubmitExamAnswersCommand request, CancellationToken cancellationToken)
        {
            // calculate score
            var total = await _resultService.CalculateTotalScoreAsync(request.ExamId, request.Answers, cancellationToken);

            // create result entity with IsFinished = true
            var result = new StudentExamResult
            {
                StudentId = request.StudentId,
                ExamId = request.ExamId,
                TotalScore = total,
                SubmittedAt = DateTime.UtcNow,
                IsFinashed = true // Mark as finished when submitted
            };

            var created = await _resultService.CreateAsync(result, cancellationToken);

            // Save individual student answers with selected options as entities
            var studentAnswers = new List<StudentAnswer>();
            foreach (var answer in request.Answers)
            {
                var studentAnswer = new StudentAnswer
                {
                    StudentExamResultId = created.Id,
                    QuestionId = answer.QuestionId,
                    TextAnswer = answer.TextAnswer,
                    IsCorrect = false // Will be calculated based on answer type
                };

                // Add selected options as entities (not JSON)
                if (answer.SelectedOptionIds != null && answer.SelectedOptionIds.Any())
                {
                    foreach (var optionId in answer.SelectedOptionIds)
                    {
                        studentAnswer.SelectedOptions.Add(new StudentAnswerOption
                        {
                            QuestionOptionId = optionId
                        });
                    }
                }

                studentAnswers.Add(studentAnswer);
            }

            // Bulk create all student answers (with cascade to options)
            if (studentAnswers.Any())
            {
                await _answerService.CreateBulkAsync(studentAnswers, cancellationToken);
            }

            // Fetch temporary image answers and convert them to permanent StudentAnswers
            var tempAnswers = await _tempAnswerService.GetByStudentAndExamAsync(request.StudentId, request.ExamId, cancellationToken);
            foreach (var tempAnswer in tempAnswers)
            {
                var permanentAnswer = new StudentAnswer
                {
                    StudentExamResultId = created.Id,
                    QuestionId = tempAnswer.QuestionId,
                    ImageAnswerUrl = tempAnswer.ImageAnswerUrl,
                    IsCorrect = false // Will be manually graded
                };

                await _answerService.CreateAsync(permanentAnswer, cancellationToken);
            }

            // Delete temporary answers after conversion
            if (tempAnswers.Any())
            {
                await _tempAnswerService.DeleteByStudentAndExamAsync(request.StudentId, request.ExamId, cancellationToken);
            }

            return Success(created.Id);
        }
    }
}
