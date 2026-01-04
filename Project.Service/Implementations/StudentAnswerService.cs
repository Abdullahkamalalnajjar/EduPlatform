using Project.Data.Entities.Exams;

namespace Project.Service.Implementations
{
    public class StudentAnswerService : IStudentAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentAnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentAnswer>> GetByStudentExamResultIdAsync(int studentExamResultId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StudentAnswers.GetTableNoTracking()
                .Include(sa => sa.Question)
                .ThenInclude(q => q.Options)
                .Include(sa => sa.SelectedOptions)
                .ThenInclude(so => so.QuestionOption)
                .Where(sa => sa.StudentExamResultId == studentExamResultId)
                .ToListAsync(cancellationToken);
        }

        public async Task<StudentAnswer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.StudentAnswers.GetTableAsTracking()
                .Include(sa => sa.Question)
                .FirstOrDefaultAsync(sa => sa.Id == id, cancellationToken);
        }

        public async Task<StudentAnswer> CreateAsync(StudentAnswer entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.StudentAnswers.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<IEnumerable<StudentAnswer>> CreateBulkAsync(IEnumerable<StudentAnswer> entities, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.StudentAnswers.AddRangeAsync(entities);
            await _unitOfWork.CompeleteAsync();
            return entities;
        }

        public async Task<StudentAnswer> UpdateAsync(StudentAnswer entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.StudentAnswers.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.StudentAnswers.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.StudentAnswers.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}
