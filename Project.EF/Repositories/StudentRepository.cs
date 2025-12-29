using Project.Data.Entities.People;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}