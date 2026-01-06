using Microsoft.EntityFrameworkCore;
using Project.Data.Entities.People;
using Project.Data.Interfaces;
using Project.Service.Abstracts;
using Project.Data.Dtos;

namespace Project.Service.Implementations
{
    public class AssistantService : IAssistantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssistantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Assistant>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Assistants.GetTableNoTracking()
                .Include(a => a.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<Assistant?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Assistants.GetTableNoTracking()
                .Include(a => a.User)
                .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<AssistantDto>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Assistants.GetTableNoTracking()
                .Include(a => a.User)
                .Include(a => a.Teacher)
                .ThenInclude(t => t.User)
                .Where(a => a.TeacherId == teacherId)
                .Select(a => new AssistantDto
                {
                    AssistantId = a.Id,
                    UserId = a.ApplicationUserId,
                    Email = a.User.Email,
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    FullName = a.User.FullName,
                    TeacherId = a.TeacherId,
                    TeacherName = a.Teacher.User.FullName
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<Assistant> CreateAsync(Assistant entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Assistants.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<Assistant> UpdateAsync(Assistant entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Assistants.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Assistants.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.Assistants.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
    }
}