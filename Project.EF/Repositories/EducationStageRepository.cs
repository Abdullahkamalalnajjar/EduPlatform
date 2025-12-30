using Project.Data.Entities.Curriculum;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class EducationStageRepository : GenericRepository<EducationStage>, IEducationStageRepository
    {
        public EducationStageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
