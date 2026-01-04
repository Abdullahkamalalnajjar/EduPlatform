using Project.Data.Entities.Exams;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class StudentAnswerOptionRepository : GenericRepository<StudentAnswerOption>, IStudentAnswerOptionRepository
    {
        public StudentAnswerOptionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
