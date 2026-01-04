using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.Exams;
using Project.Data.Interfaces;
using Project.Service.Abstracts;

namespace Project.Service.Implementations
{
    public class TemporaryStudentAnswerService : ITemporaryStudentAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TemporaryStudentAnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TemporaryStudentAnswer>> GetByStudentAndExamAsync(int studentId, int examId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.TemporaryStudentAnswers.GetTableNoTracking()
                .Include(ta => ta.Question)
                .Where(ta => ta.StudentId == studentId && ta.ExamId == examId)
                .ToListAsync(cancellationToken);
        }

        public async Task<TemporaryStudentAnswer> CreateAsync(TemporaryStudentAnswer entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.TemporaryStudentAnswers.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.TemporaryStudentAnswers.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.TemporaryStudentAnswers.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }

        public async Task DeleteByStudentAndExamAsync(int studentId, int examId, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.TemporaryStudentAnswers.GetTableAsTracking()
                .Where(ta => ta.StudentId == studentId && ta.ExamId == examId)
                .ToListAsync(cancellationToken);

            foreach (var entity in entities)
            {
                await _unitOfWork.TemporaryStudentAnswers.Delete(entity);
            }

            await _unitOfWork.CompeleteAsync();
        }
    }
}
