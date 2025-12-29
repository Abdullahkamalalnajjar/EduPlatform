using Project.Data.Entities.Exams;

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
    }
}