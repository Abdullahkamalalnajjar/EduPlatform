using Project.Data.Entities.Content;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class LectureRepository : GenericRepository<Lecture>, ILectureRepository
    {
        public LectureRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}