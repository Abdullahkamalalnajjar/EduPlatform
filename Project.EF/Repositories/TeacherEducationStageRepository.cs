using Project.Data.Entities.People;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class TeacherEducationStageRepository : GenericRepository<TeacherEducationStage>, ITeacherEducationStageRepository
    {
        public TeacherEducationStageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
