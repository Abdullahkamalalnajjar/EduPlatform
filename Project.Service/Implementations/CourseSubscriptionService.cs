using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.Subscriptions;
using Project.Data.Interfaces;
using Project.Service.Abstracts;

namespace Project.Service.Implementations
{
    public class CourseSubscriptionService : ICourseSubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseSubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CourseSubscription>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CourseSubscriptions.GetTableNoTracking()
                .Include(cs => cs.Course)
                .Include(cs => cs.Student)
                .ToListAsync(cancellationToken);
        }

        public async Task<CourseSubscription?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CourseSubscriptions.GetTableNoTracking()
                .Include(cs => cs.Course)
                .Include(cs => cs.Student)
                .SingleOrDefaultAsync(cs => cs.Id == id, cancellationToken);
        }

        public async Task<CourseSubscription> CreateAsync(CourseSubscription entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.CourseSubscriptions.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<CourseSubscription> UpdateAsync(CourseSubscription entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CourseSubscriptions.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.CourseSubscriptions.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.CourseSubscriptions.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}