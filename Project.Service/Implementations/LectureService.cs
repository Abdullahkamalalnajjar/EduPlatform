using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.Content;
using Project.Data.Interfaces;
using Project.Service.Abstracts;

namespace Project.Service.Implementations
{
    public class LectureService : ILectureService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LectureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Lecture>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Lectures.GetTableNoTracking()
                .Include(l => l.Course)
                .Include(l => l.Materials)
                .ToListAsync(cancellationToken);
        }

        public async Task<Lecture?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Lectures.GetTableNoTracking()
                .Include(l => l.Course)
                .Include(l => l.Materials)
                .SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
        }

        public async Task<Lecture> CreateAsync(Lecture entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Lectures.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<Lecture> UpdateAsync(Lecture entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Lectures.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Lectures.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.Lectures.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}