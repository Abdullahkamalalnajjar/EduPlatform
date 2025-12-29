using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.Exams;
using Project.Data.Interfaces;
using Project.Service.Abstracts;

namespace Project.Service.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Question>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Questions.GetTableNoTracking()
                .Include(q => q.Options)
                .ToListAsync(cancellationToken);
        }

        public async Task<Question?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Questions.GetTableNoTracking()
                .Include(q => q.Options)
                .SingleOrDefaultAsync(q => q.Id == id, cancellationToken);
        }

        public async Task<Question> CreateAsync(Question entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Questions.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<Question> UpdateAsync(Question entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Questions.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Questions.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.Questions.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}