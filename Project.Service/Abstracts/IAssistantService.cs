using Project.Data.Entities.People;
using Project.Data.Dtos;

namespace Project.Service.Abstracts
{
    public interface IAssistantService
    {
        Task<IEnumerable<Assistant>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Assistant?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<AssistantDto>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default);
        Task<Assistant> CreateAsync(Assistant entity, CancellationToken cancellationToken = default);
        Task<Assistant> UpdateAsync(Assistant entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}