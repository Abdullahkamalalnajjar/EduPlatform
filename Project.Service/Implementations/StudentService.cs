using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.People;
using Project.Data.Interfaces;
using Project.Service.Abstracts;

namespace Project.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Students.GetTableNoTracking()
                .Include(s => s.User)
                .Include(s => s.CourseSubscriptions)
                .ToListAsync(cancellationToken);
        }

        public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Students.GetTableNoTracking()
                .Include(s => s.User)
                .Include(s => s.CourseSubscriptions)
                .SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Student> CreateAsync(Student entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Students.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<Student> UpdateAsync(Student entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Students.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Students.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.Students.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}