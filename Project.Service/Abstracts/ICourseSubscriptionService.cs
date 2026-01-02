using Project.Data.Entities.Subscriptions;

namespace Project.Service.Abstracts
{
    public interface ICourseSubscriptionService
    {
        Task<IEnumerable<CourseSubscriptionDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CourseSubscription?> GetByIdForEditAsync(int id, CancellationToken cancellationToken = default);
        Task<CourseSubscriptionDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CourseSubscriptionDto>> GetByStudentIdAndStatusAsync(int studentId, string status, CancellationToken cancellationToken = default);
        Task<CourseSubscription> CreateAsync(CourseSubscription entity, CancellationToken cancellationToken = default);
        Task<CourseSubscription> UpdateAsync(CourseSubscription entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}