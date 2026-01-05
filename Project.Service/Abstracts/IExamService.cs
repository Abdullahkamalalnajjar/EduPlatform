using Project.Data.Entities.Exams;
using Project.Data.Dtos;

namespace Project.Service.Abstracts
{
    public interface IExamService
    {
        Task<IEnumerable<Exam>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Exam?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Exam?> GetByLectureIdAsync(int lectureId, CancellationToken cancellationToken = default);
        Task<IEnumerable<StudentExamSubmissionDto>> GetStudentSubmissionsByLectureAsync(int lectureId, CancellationToken cancellationToken = default);
        Task<Exam> CreateAsync(Exam entity, CancellationToken cancellationToken = default);
        Task<Exam> UpdateAsync(Exam entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}