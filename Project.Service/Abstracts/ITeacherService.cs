using Project.Data.Entities.People;

namespace Project.Service.Abstracts
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Teacher?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Teacher> CreateAsync(Teacher entity, CancellationToken cancellationToken = default);
        Task<Teacher> UpdateAsync(Teacher entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Teacher>> GetByGradeYearAndSubjectAsync(int gradeYear, int subjectId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Teacher>> GetByEducationStageAndSubjectAsync(int educationStageId, int subjectId, CancellationToken cancellationToken = default);
    }
}