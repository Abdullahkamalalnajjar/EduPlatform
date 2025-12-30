using Project.Data.Entities.People;

namespace Project.Service.Abstracts
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Student> CreateAsync(Student entity, CancellationToken cancellationToken = default);
        Task<Student> UpdateAsync(Student entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    }
}