using Project.Data.Entities.Curriculum;

namespace Project.Service.Abstracts
{
    public interface IEducationStageService
    {
        Task<IEnumerable<EducationStage>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<EducationStage?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<EducationStage> CreateAsync(EducationStage entity, CancellationToken cancellationToken = default);
        Task<EducationStage> UpdateAsync(EducationStage entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
