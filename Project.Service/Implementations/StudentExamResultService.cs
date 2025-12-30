using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.Exams;
using Project.Data.Interfaces;
using Project.Service.Abstracts;
using Project.Data.Dtos;

namespace Project.Service.Implementations
{
    public class StudentExamResultService : IStudentExamResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentExamResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentExamResult>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StudentExamResults.GetTableNoTracking()
                .Include(r => r.Student)
                .Include(r => r.Exam)
                .ToListAsync(cancellationToken);
        }

        public async Task<StudentExamResult?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StudentExamResults.GetTableNoTracking()
                .Include(r => r.Student)
                .Include(r => r.Exam)
                .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<StudentExamResult> CreateAsync(StudentExamResult entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.StudentExamResults.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<StudentExamResult> UpdateAsync(StudentExamResult entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.StudentExamResults.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.StudentExamResults.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.StudentExamResults.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }

        public async Task<int> CalculateTotalScoreAsync(int examId, IEnumerable<StudentAnswerDto> answers, CancellationToken cancellationToken = default)
        {
            // Load questions and their options for the exam
            var questions = await _unitOfWork.Questions.GetTableNoTracking()
                .Include(q => q.Options)
                .Where(q => q.ExamId == examId)
                .ToListAsync(cancellationToken);

            var answerMap = answers.ToDictionary(a => a.QuestionId, a => a);

            int total = 0;

            foreach (var question in questions)
            {
                // No answer provided
                if (!answerMap.TryGetValue(question.Id, out var studentAnswer))
                    continue;

                // handle MCQ by comparing selected option ids to correct option ids
                if (string.Equals(question.AnswerType, "MCQ", StringComparison.OrdinalIgnoreCase))
                {
                    var correctOptionIds = question.Options.Where(o => o.IsCorrect).Select(o => o.Id).OrderBy(i => i).ToList();
                    var selected = studentAnswer.SelectedOptionIds.OrderBy(i => i).ToList();
                    if (correctOptionIds.SequenceEqual(selected))
                    {
                        total += question.Score;
                    }
                }
                else if (string.Equals(question.AnswerType, "TextAnswer", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(question.AnswerType, "ImageAnswer", StringComparison.OrdinalIgnoreCase))
                {
                    // Text/Image answers require manual grading. For now, treat as 0.
                }
            }

            return total;
        }
    }
}