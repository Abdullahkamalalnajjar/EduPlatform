using Project.Data.Entities.Curriculum;

namespace Project.Service.Implementations
{
    public class EducationStageService : IEducationStageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EducationStageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EducationStage>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.EducationStages.GetTableNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<EducationStage?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.EducationStages.GetTableNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<EducationStage> CreateAsync(EducationStage entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.EducationStages.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<EducationStage> UpdateAsync(EducationStage entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.EducationStages.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.EducationStages.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.EducationStages.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}
