using Project.Core.Features.Parents.Queries.Models;

namespace Project.Core.Features.Parents.Queries.Handlers
{
    public class GetStudentCourseExamScoresQueryHandler : ResponseHandler, IRequestHandler<GetStudentCourseExamScoresQuery, Response<IEnumerable<StudentCourseExamDto>>>
    {
        private readonly IStudentExamResultService _resultService;
        private readonly IStudentAnswerService _answerService;
        private readonly IUnitOfWork _unitOfWork;

        public GetStudentCourseExamScoresQueryHandler(IStudentExamResultService resultService, IStudentAnswerService answerService, IUnitOfWork unitOfWork)
        {
            _resultService = resultService;
            _answerService = answerService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<StudentCourseExamDto>>> Handle(GetStudentCourseExamScoresQuery request, CancellationToken cancellationToken)
        {
            // Get all exams for the course
            var exams = await _unitOfWork.Exams.GetTableNoTracking()
                .Include(e => e.Questions)
                .Where(e => e.Lecture.CourseId == request.CourseId)
                .ToListAsync(cancellationToken);

            if (!exams.Any())
                return NotFound<IEnumerable<StudentCourseExamDto>>("No exams found for this course");

            // Get exam results for the student
            var results = await _resultService.GetAllAsync(cancellationToken);
            var studentResults = results.Where(r => r.StudentId == request.StudentId && r.IsFinashed).ToList();

            var dtos = new List<StudentCourseExamDto>();

            foreach (var exam in exams)
            {
                var result = studentResults.FirstOrDefault(r => r.ExamId == exam.Id);
                if (result != null)
                {
                    var answers = await _answerService.GetByStudentExamResultIdAsync(result.Id, cancellationToken);
                    var maxScore = exam.Questions?.Sum(q => q.Score) ?? 0;
                    var correctAnswers = answers.Count(a => a.IsCorrect);
                    // round percentage to 2 decimal places

                    var percentage = maxScore > 0 ? (decimal)result.TotalScore / maxScore * 100 : 0;

                    dtos.Add(new StudentCourseExamDto
                    {
                        ExamId = exam.Id,
                        ExamTitle = exam.Title,
                        TotalScore = result.TotalScore,
                        MaxScore = maxScore,
                        IsFinished = result.IsFinashed,
                        SubmittedAt = result.SubmittedAt,
                        CorrectAnswers = correctAnswers,
                        TotalQuestions = answers.Count(),
                        Percentage = Math.Round(percentage, 2)
                    });
                }
            }

            if (!dtos.Any())
                return NotFound<IEnumerable<StudentCourseExamDto>>("No exam results found for this student in this course");

            return Success(dtos.AsEnumerable());
        }
    }
}
