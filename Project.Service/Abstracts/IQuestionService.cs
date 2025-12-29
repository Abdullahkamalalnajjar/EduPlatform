using Project.Data.Entities.Exams;

namespace Project.Service.Abstracts
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Question?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Question> CreateAsync(Question entity, CancellationToken cancellationToken = default);
        Task<Question> UpdateAsync(Question entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}