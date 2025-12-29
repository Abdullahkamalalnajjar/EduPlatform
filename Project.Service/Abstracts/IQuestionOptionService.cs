using Project.Data.Entities.Exams;

namespace Project.Service.Abstracts
{
    public interface IQuestionOptionService
    {
        Task<IEnumerable<QuestionOption>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<QuestionOption?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<QuestionOption> CreateAsync(QuestionOption entity, CancellationToken cancellationToken = default);
        Task<QuestionOption> UpdateAsync(QuestionOption entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}