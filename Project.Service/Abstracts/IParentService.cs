using Project.Data.Entities.People;

namespace Project.Service.Abstracts
{
    public interface IParentService
    {
        Task<IEnumerable<Parent>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Parent?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Parent> CreateAsync(Parent entity, CancellationToken cancellationToken = default);
        Task<Parent> UpdateAsync(Parent entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}