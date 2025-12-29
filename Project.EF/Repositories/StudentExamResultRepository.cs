using Project.Data.Entities.Exams;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class StudentExamResultRepository : GenericRepository<StudentExamResult>, IStudentExamResultRepository
    {
        public StudentExamResultRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}