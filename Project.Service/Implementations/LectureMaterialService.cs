using Project.Data.Entities.Content;

namespace Project.Service.Implementations
{
    public class LectureMaterialService : ILectureMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LectureMaterialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LectureMaterial>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.LectureMaterials.GetTableNoTracking()
                .Include(lm => lm.Lecture)
                .ToListAsync(cancellationToken);
        }

        public async Task<LectureMaterial?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.LectureMaterials.GetTableNoTracking()
                .Include(lm => lm.Lecture)
                .SingleOrDefaultAsync(lm => lm.Id == id, cancellationToken);
        }

        public async Task<LectureMaterial> CreateAsync(LectureMaterial entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.LectureMaterials.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<LectureMaterial> UpdateAsync(LectureMaterial entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.LectureMaterials.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.LectureMaterials.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.LectureMaterials.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}