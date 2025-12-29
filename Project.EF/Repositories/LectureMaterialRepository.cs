using Project.Data.Entities.Content;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class LectureMaterialRepository : GenericRepository<LectureMaterial>, ILectureMaterialRepository
    {
        public LectureMaterialRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}