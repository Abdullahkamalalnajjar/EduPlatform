using Project.Data.Entities.Exams;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class StudentAnswerRepository : GenericRepository<StudentAnswer>, IStudentAnswerRepository
    {
        public StudentAnswerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
