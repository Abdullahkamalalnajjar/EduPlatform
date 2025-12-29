using Project.Data.Entities.Content;

namespace Project.Service.Abstracts
{
    public interface ILectureMaterialService
    {
        Task<IEnumerable<LectureMaterial>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<LectureMaterial?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<LectureMaterial> CreateAsync(LectureMaterial entity, CancellationToken cancellationToken = default);
        Task<LectureMaterial> UpdateAsync(LectureMaterial entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}