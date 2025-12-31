using Project.Data.Entities.Curriculum;

namespace Project.Service.Abstracts
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Course?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Course> CreateAsync(Course entity, CancellationToken cancellationToken = default);
        Task<Course> UpdateAsync(Course entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CourseDto>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default);
    }
}