using Project.Data.Entities.Exams;

namespace Project.Service.Implementations
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Exam>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Exams.GetTableNoTracking()
                .Include(e => e.Lecture)
                .Include(e => e.Questions)
                .ThenInclude(o => o.Options)
                .ToListAsync(cancellationToken);
        }

        public async Task<Exam?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Exams.GetTableNoTracking()
                .Include(e => e.Lecture)
                .Include(e => e.Questions)
                .ThenInclude(o => o.Options)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<Exam?> GetByLectureIdAsync(int lectureId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Exams.GetTableNoTracking()
                .Include(e => e.Lecture)
                .SingleOrDefaultAsync(e => e.LectureId == lectureId, cancellationToken);
        }

        public async Task<IEnumerable<StudentExamSubmissionDto>> GetStudentSubmissionsByLectureAsync(int lectureId, CancellationToken cancellationToken = default)
        {
            // Get exam for this lecture
            var exam = await _unitOfWork.Exams.GetTableNoTracking()
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(e => e.LectureId == lectureId, cancellationToken);

            if (exam == null)
                return Enumerable.Empty<StudentExamSubmissionDto>();

            // Get all exam submissions for this exam
            var submissions = await _unitOfWork.StudentExamResults.GetTableNoTracking()
                .Include(r => r.Student)
                .ThenInclude(s => s.User)
                .Include(r => r.Exam)
                .Where(r => r.ExamId == exam.Id && r.IsFinashed)
                .ToListAsync(cancellationToken);

            if (!submissions.Any())
                return Enumerable.Empty<StudentExamSubmissionDto>();

            var maxScore = exam.Questions?.Sum(q => q.Score) ?? 0;

            var result = new List<StudentExamSubmissionDto>();

            foreach (var submission in submissions)
            {
                // Get all answers for this submission
                var answers = await _unitOfWork.StudentAnswers.GetTableNoTracking()
                    .Where(a => a.StudentExamResultId == submission.Id)
                    .ToListAsync(cancellationToken);

                var manuallyGraded = answers.Count(a => a.PointsEarned.HasValue);
                var pendingGrading = answers.Count(a => !a.PointsEarned.HasValue && string.IsNullOrEmpty(a.TextAnswer) && string.IsNullOrEmpty(a.ImageAnswerUrl));

                result.Add(new StudentExamSubmissionDto
                {
                    StudentExamResultId = submission.Id,
                    StudentId = submission.StudentId,
                    StudentName = submission.Student?.User?.FullName ?? "Unknown",
                    StudentEmail = submission.Student?.User?.Email ?? "Unknown",
                    ExamId = exam.Id,
                    ExamTitle = exam.Title,
                    CurrentTotalScore = submission.TotalScore,
                    MaxScore = maxScore,
                    IsFinished = submission.IsFinashed,
                    SubmittedAt = submission.SubmittedAt,
                    TotalAnswers = answers.Count,
                    ManuallyGradedAnswers = manuallyGraded,
                    PendingGradingAnswers = pendingGrading
                });
            }

            return result;
        }

        public async Task<Exam> CreateAsync(Exam entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Exams.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<Exam> UpdateAsync(Exam entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Exams.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Exams.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.Exams.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}