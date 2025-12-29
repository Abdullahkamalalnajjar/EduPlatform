using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.People;
using Project.Data.Interfaces;
using Project.Service.Abstracts;

namespace Project.Service.Implementations
{
    public class ParentService : IParentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Parent>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Parents.GetTableNoTracking()
                .Include(p => p.User)
                .Include(p => p.Children)
                .ToListAsync(cancellationToken);
        }

        public async Task<Parent?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Parents.GetTableNoTracking()
                .Include(p => p.User)
                .Include(p => p.Children)
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<Parent> CreateAsync(Parent entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Parents.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<Parent> UpdateAsync(Parent entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Parents.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Parents.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.Parents.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}