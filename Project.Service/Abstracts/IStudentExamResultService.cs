using Project.Data.Entities.Exams;
using Project.Data.Dtos;

namespace Project.Service.Abstracts
{
    public interface IStudentExamResultService
    {
        Task<IEnumerable<StudentExamResult>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<StudentExamResult?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<StudentExamResult> CreateAsync(StudentExamResult entity, CancellationToken cancellationToken = default);
        Task<StudentExamResult> UpdateAsync(StudentExamResult entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task<int> CalculateTotalScoreAsync(int examId, IEnumerable<StudentAnswerDto> answers, CancellationToken cancellationToken = default);
    }
}