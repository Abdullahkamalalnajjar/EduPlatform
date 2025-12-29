using Project.Data.Entities.People;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}