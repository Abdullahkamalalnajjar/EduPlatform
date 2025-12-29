using Project.Data.Entities.Curriculum;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}