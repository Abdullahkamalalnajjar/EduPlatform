using Project.Core.Features.Exams.Commands.Models;

namespace Project.Core.Features.Exams.Commands.Handlers
{
    public class GradeExamCommandHandler : ResponseHandler, IRequestHandler<GradeExamCommand, Response<GradeExamResponse>>
    {
        private readonly IStudentExamResultService _resultService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStudentAnswerService _answerService;
        private readonly IUnitOfWork _unitOfWork;

        public GradeExamCommandHandler(IStudentExamResultService resultService,ICurrentUserService currentUserService, IStudentAnswerService answerService, IUnitOfWork unitOfWork)
        {
            _resultService = resultService;
            _currentUserService = currentUserService;
            _answerService = answerService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GradeExamResponse>> Handle(GradeExamCommand request, CancellationToken cancellationToken)
        {
            // Get the current user (grader)
            var graderId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(graderId))
                return BadRequest<GradeExamResponse>("Unable to identify the grader");

            // Validate request
            if (request.StudentExamResultId <= 0)
                return BadRequest<GradeExamResponse>("Invalid exam result ID");

            if (request.GradedAnswers == null || !request.GradedAnswers.Any())
                return BadRequest<GradeExamResponse>("No graded answers provided");

            // Get the exam result - use tracking to attach it for update
            var examResult = await _unitOfWork.StudentExamResults.GetTableAsTracking()
                .FirstOrDefaultAsync(r => r.Id == request.StudentExamResultId, cancellationToken);

            if (examResult is null)
                return NotFound<GradeExamResponse>("Exam result not found");

            var previousTotalScore = examResult.TotalScore;
            decimal additionalPoints = 0;
            var gradedCount = 0;
            var invalidAnswers = new List<string>();

            // Grade each answer
            foreach (var gradedAnswer in request.GradedAnswers)
            {
                // Validate studentAnswerId
                if (gradedAnswer.StudentAnswerId <= 0)
                {
                    invalidAnswers.Add($"Invalid answer ID: {gradedAnswer.StudentAnswerId}");
                    continue;
                }

                var studentAnswer = await _answerService.GetByIdAsync(gradedAnswer.StudentAnswerId, cancellationToken);
                if (studentAnswer is null)
                {
                    invalidAnswers.Add($"Answer {gradedAnswer.StudentAnswerId} not found");
                    continue;
                }

                // Only update if not already manually graded
                if (!studentAnswer.PointsEarned.HasValue || studentAnswer.PointsEarned == 0)
                {
                    studentAnswer.PointsEarned = gradedAnswer.PointsEarned;
                    studentAnswer.IsCorrect = gradedAnswer.IsCorrect;
                    studentAnswer.Feedback = gradedAnswer.Feedback; // ??? ?????????
                    studentAnswer.GradedByUserId = graderId; // ????? ??????

                    await _answerService.UpdateAsync(studentAnswer, cancellationToken);
                    additionalPoints += gradedAnswer.PointsEarned;
                    gradedCount++;
                }
            }

            // Check if any answers were successfully graded
            if (gradedCount == 0 && invalidAnswers.Count > 0)
                return BadRequest<GradeExamResponse>($"Failed to grade exam. Errors: {string.Join(", ", invalidAnswers)}");

            // Update the total score with the manually graded points
            var newTotalScore = previousTotalScore + (int)additionalPoints;
            examResult.TotalScore = newTotalScore;

            // Update using the unit of work directly since we already have the tracked entity
            _unitOfWork.StudentExamResults.Update(examResult);
            await _unitOfWork.CompeleteAsync();

            var message = $"Exam graded successfully. Graded {gradedCount} answer(s)";
            if (invalidAnswers.Count > 0)
                message += $". (Warnings: {string.Join(", ", invalidAnswers)})";
            message += $". Previous score: {previousTotalScore}, New score: {newTotalScore}";

            var response = new GradeExamResponse
            {
                StudentExamResultId = request.StudentExamResultId,
                PreviousTotalScore = previousTotalScore,
                NewTotalScore = newTotalScore,
                PointsFromManualGrading = additionalPoints,
                Message = message
            };

            return Success(response);
        }
    }
}
