using Project.Core.Features.Exams.Commands.Models;
using Project.Data.Entities.Exams;
using Project.Service.Abstracts;

namespace Project.Core.Features.Exams.Commands.Handlers
{
    public class GradeExamCommandHandler : ResponseHandler, IRequestHandler<GradeExamCommand, Response<GradeExamResponse>>
    {
        private readonly IStudentExamResultService _resultService;
        private readonly IStudentAnswerService _answerService;

        public GradeExamCommandHandler(IStudentExamResultService resultService, IStudentAnswerService answerService)
        {
            _resultService = resultService;
            _answerService = answerService;
        }

        public async Task<Response<GradeExamResponse>> Handle(GradeExamCommand request, CancellationToken cancellationToken)
        {
            // Get the exam result
            var examResult = await _resultService.GetByIdAsync(request.StudentExamResultId, cancellationToken);
            if (examResult is null)
                return NotFound<GradeExamResponse>("Exam result not found");

            var previousTotalScore = examResult.TotalScore;
            int additionalPoints = 0;

            // Grade each answer
            foreach (var gradedAnswer in request.GradedAnswers)
            {
                var studentAnswer = await _answerService.GetByIdAsync(gradedAnswer.StudentAnswerId, cancellationToken);
                if (studentAnswer is null)
                    continue;

                // Update the answer with grading information
                studentAnswer.PointsEarned = gradedAnswer.PointsEarned;
                studentAnswer.IsCorrect = gradedAnswer.IsCorrect;
                
                await _answerService.UpdateAsync(studentAnswer, cancellationToken);
                additionalPoints += gradedAnswer.PointsEarned;
            }

            // Update the total score with the manually graded points
            examResult.TotalScore = previousTotalScore + additionalPoints;
            await _resultService.UpdateAsync(examResult, cancellationToken);

            var response = new GradeExamResponse
            {
                StudentExamResultId = request.StudentExamResultId,
                PreviousTotalScore = previousTotalScore,
                NewTotalScore = examResult.TotalScore,
                PointsFromManualGrading = additionalPoints,
                Message = $"?? ????? ???????? ?????. ?????? ???????: {previousTotalScore}? ?????? ???????: {examResult.TotalScore}"
            };

            return Success(response);
        }
    }
}
