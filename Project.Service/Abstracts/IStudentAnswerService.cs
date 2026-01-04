using Project.Data.Entities.Exams;

namespace Project.Service.Abstracts
{
    public interface IStudentAnswerService
    {
        Task<IEnumerable<StudentAnswer>> GetByStudentExamResultIdAsync(int studentExamResultId, CancellationToken cancellationToken = default);
        Task<StudentAnswer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<StudentAnswer> CreateAsync(StudentAnswer entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<StudentAnswer>> CreateBulkAsync(IEnumerable<StudentAnswer> entities, CancellationToken cancellationToken = default);
        Task<StudentAnswer> UpdateAsync(StudentAnswer entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
