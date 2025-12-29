using Project.Data.Entities.Content;

namespace Project.Service.Abstracts
{
    public interface ILectureService
    {
        Task<IEnumerable<Lecture>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Lecture?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Lecture> CreateAsync(Lecture entity, CancellationToken cancellationToken = default);
        Task<Lecture> UpdateAsync(Lecture entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}