using Project.Data.Entities.Subscriptions;

namespace Project.Service.Abstracts
{
    public interface ICourseSubscriptionService
    {
        Task<IEnumerable<CourseSubscription>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CourseSubscription?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<CourseSubscription> CreateAsync(CourseSubscription entity, CancellationToken cancellationToken = default);
        Task<CourseSubscription> UpdateAsync(CourseSubscription entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}