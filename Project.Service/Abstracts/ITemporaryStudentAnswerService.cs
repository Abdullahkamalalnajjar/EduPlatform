using Project.Data.Entities.Exams;

namespace Project.Service.Abstracts
{
    public interface ITemporaryStudentAnswerService
    {
        Task<IEnumerable<TemporaryStudentAnswer>> GetByStudentAndExamAsync(int studentId, int examId, CancellationToken cancellationToken = default);
        Task<TemporaryStudentAnswer> CreateAsync(TemporaryStudentAnswer entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteByStudentAndExamAsync(int studentId, int examId, CancellationToken cancellationToken = default);
    }
}
