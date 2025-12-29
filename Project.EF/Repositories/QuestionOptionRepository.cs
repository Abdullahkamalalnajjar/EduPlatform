using Project.Data.Entities.Exams;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class QuestionOptionRepository : GenericRepository<QuestionOption>, IQuestionOptionRepository
    {
        public QuestionOptionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}