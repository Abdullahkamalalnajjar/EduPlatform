using Project.Data.Entities.People;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class AssistantRepository : GenericRepository<Assistant>, IAssistantRepository
    {
        public AssistantRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}