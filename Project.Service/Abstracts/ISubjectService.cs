using Project.Data.Entities.Curriculum;

namespace Project.Service.Abstracts
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Subject?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<SubjectDto>> GetTeacherWithCourseBySubjectId(int curriculumId, CancellationToken cancellationToken = default);
        Task<Subject> CreateAsync(Subject entity, CancellationToken cancellationToken = default);
        Task<Subject> UpdateAsync(Subject entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}