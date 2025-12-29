using Project.Data.Entities.People;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class ParentRepository : GenericRepository<Parent>, IParentRepository
    {
        public ParentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}