using Project.Data.Entities.People;

namespace Project.Service.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Teachers.GetTableNoTracking()
                .Include(t => t.User)
                .Include(t => t.Courses)
                .ThenInclude(c => c.Lectures)
                .ToListAsync(cancellationToken);
        }

        public async Task<Teacher?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Teachers.GetTableNoTracking()
                .Include(t => t.User)
                .Include(t => t.Courses)
                .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<Teacher> CreateAsync(Teacher entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Teachers.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<Teacher> UpdateAsync(Teacher entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Teachers.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.Teachers.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }

        public async Task<IEnumerable<Teacher>> GetByGradeYearAndSubjectAsync(int gradeYear, int subjectId, CancellationToken cancellationToken = default)
        {
            // Find teachers who teach courses that match gradeYear and have the provided subjectId
            return await _unitOfWork.Teachers.GetTableNoTracking()
                .Include(t => t.User)
                .Include(t => t.Courses)
                .Where(t => t.Subject.Id == subjectId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Teacher>> GetByEducationStageAndSubjectAsync(int educationStageId, int subjectId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.TeacherEducationStages.GetTableNoTracking()
                .Include(ts => ts.Teacher)
                    .ThenInclude(t => t.User)
                .Include(ts => ts.Teacher)
                    .ThenInclude(t => t.Subject)
                .Include(ts => ts.Teacher)
                    .ThenInclude(t => t.Courses)
                        .ThenInclude(c => c.EducationStage)
                .Include(ts => ts.Teacher)
                    .ThenInclude(t => t.Courses)
                        .ThenInclude(c => c.Lectures)
                            .ThenInclude(l => l.Materials)
                .Where(ts => ts.EducationStageId == educationStageId && ts.Teacher.SubjectId == subjectId)
                .Select(ts => ts.Teacher)
                .Distinct()
                .ToListAsync(cancellationToken);
        }
    }
}