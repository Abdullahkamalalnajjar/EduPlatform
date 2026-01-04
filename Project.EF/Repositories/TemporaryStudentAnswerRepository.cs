using Project.Data.Entities.Exams;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class TemporaryStudentAnswerRepository : GenericRepository<TemporaryStudentAnswer>, ITemporaryStudentAnswerRepository
    {
        public TemporaryStudentAnswerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
