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