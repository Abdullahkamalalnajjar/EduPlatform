using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.Exams;
using Project.Data.Interfaces;
using Project.Service.Abstracts;

namespace Project.Service.Implementations
{
    public class QuestionOptionService : IQuestionOptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionOptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<QuestionOption>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.QuestionOptions.GetTableNoTracking()
                .Include(qo => qo.Question)
                .ToListAsync(cancellationToken);
        }

        public async Task<QuestionOption?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.QuestionOptions.GetTableNoTracking()
                .Include(qo => qo.Question)
                .SingleOrDefaultAsync(qo => qo.Id == id, cancellationToken);
        }

        public async Task<QuestionOption> CreateAsync(QuestionOption entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.QuestionOptions.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<QuestionOption> UpdateAsync(QuestionOption entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.QuestionOptions.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.QuestionOptions.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.QuestionOptions.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}