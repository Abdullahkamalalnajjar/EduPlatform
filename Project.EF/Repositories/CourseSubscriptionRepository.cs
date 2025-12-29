using Project.Data.Entities.Subscriptions;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class CourseSubscriptionRepository : GenericRepository<CourseSubscription>, ICourseSubscriptionRepository
    {
        public CourseSubscriptionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}