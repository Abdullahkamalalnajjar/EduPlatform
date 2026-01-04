using MediatR;
using Project.Core.Features.StudentAnswers.Commands.Models;
using Project.Data.Entities.Exams;
using Project.Service.Abstracts;

namespace Project.Core.Features.StudentAnswers.Commands.Handlers
{
    public class SubmitImageAnswerCommandHandler : ResponseHandler, IRequestHandler<SubmitImageAnswerCommand, Response<int>>
    {
        private readonly IStudentAnswerService _answerService;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public SubmitImageAnswerCommandHandler(IStudentAnswerService answerService, IFileService fileService, IUnitOfWork unitOfWork)
        {
            _answerService = answerService;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(SubmitImageAnswerCommand request, CancellationToken cancellationToken)
        {
            // Validate that question exists
            if (request.QuestionId <= 0)
                return BadRequest<int>("Invalid question ID");

            if (request.ImageFile == null || request.ImageFile.Length == 0)
                return BadRequest<int>("Image file is required");

            // Upload the image
            var imageUrl = await _fileService.UploadImage("student-answers", request.ImageFile);
            if (string.IsNullOrWhiteSpace(imageUrl) || imageUrl == "FailedToUploadImage" || imageUrl == "NoImage")
            {
                return BadRequest<int>("Failed to upload image answer");
            }

            // If StudentExamResultId exists, save directly to StudentAnswer
            if (request.StudentExamResultId > 0)
            {
                var studentAnswer = new StudentAnswer
                {
                    StudentExamResultId = request.StudentExamResultId,
                    QuestionId = request.QuestionId,
                    ImageAnswerUrl = imageUrl,
                    IsCorrect = false // Will be manually graded
                };

                var created = await _answerService.CreateAsync(studentAnswer, cancellationToken);
                return Success(created.Id);
            }
            else
            {
                // If StudentExamResultId doesn't exist yet, save to temporary storage
                var tempAnswer = new TemporaryStudentAnswer
                {
                    ExamId = request.ExamId,
                    StudentId = request.StudentId,
                    QuestionId = request.QuestionId,
                    ImageAnswerUrl = imageUrl,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.TemporaryStudentAnswers.AddAsync(tempAnswer, cancellationToken);
                await _unitOfWork.CompeleteAsync();

                return Success(tempAnswer.Id);
            }
        }
    }
}
