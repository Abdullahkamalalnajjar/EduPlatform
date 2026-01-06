using Project.Data.Entities.People;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
