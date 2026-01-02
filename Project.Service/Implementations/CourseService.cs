using Project.Data.Entities.Curriculum;

namespace Project.Service.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Courses.GetTableNoTracking()
                .Include(c => c.Teacher)
                .Include(c => c.Lectures)
                .ToListAsync(cancellationToken);
        }

        public async Task<Course?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Courses.GetTableNoTracking()
                .Include(c => c.Teacher)
                .Include(c => c.Lectures)
                .Include(e => e.EducationStage)
                .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Course> CreateAsync(Course entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Courses.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<Course> UpdateAsync(Course entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Courses.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Courses.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.Courses.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }

        public async Task<IEnumerable<CourseDto>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Courses.GetTableNoTracking()
                .Where(c => c.TeacherId == teacherId)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    TeacherId = c.TeacherId,
                    TeacherName = c.Teacher.User.FullName,
                    EducationStageId = c.EducationStageId,
                    EducationStageName = c.EducationStage.Name,
                    CourseImageUrl = c.CourseImageUrl,
                    Lectures = c.Lectures.Select(l => new LectureDto
                    {
                        Id = l.Id,
                        Title = l.Title,
                        Materials = l.Materials.Select(m => new MaterialDto
                        {
                            Id = m.Id,
                            Type = m.Type,
                            Title = m.Title,
                            FileUrl = m.FileUrl,
                            IsFree = m.IsFree
                        }).ToList()
                    }).ToList()

                })
                .ToListAsync(cancellationToken);
        }
    }
}