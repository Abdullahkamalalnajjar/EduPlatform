using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.Curriculum;
using Project.Data.Interfaces;
using Project.Service.Abstracts;

namespace Project.Service.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Subjects.GetTableNoTracking()
                .Include(s => s.Teachers)
                .ToListAsync(cancellationToken);
        }

        public async Task<Subject?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Subjects.GetTableNoTracking()
                .Include(s => s.Teachers)
                .SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Subject> CreateAsync(Subject entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Subjects.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<Subject> UpdateAsync(Subject entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Subjects.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.Subjects.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}