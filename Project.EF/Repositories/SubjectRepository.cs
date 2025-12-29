using Project.Data.Entities.Curriculum;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}