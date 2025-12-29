using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.People;
using Project.Data.Interfaces;
using Project.Service.Abstracts;

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
    }
}