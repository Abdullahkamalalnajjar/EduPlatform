using Project.Data.Entities.Subscriptions;
using System.Linq.Expressions;

namespace Project.Service.Implementations
{
    public class CourseSubscriptionService : ICourseSubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseSubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CourseSubscriptionDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CourseSubscriptions.GetTableNoTracking()
                  .Select(ToCourseSubscriptionDto)
                .ToListAsync(cancellationToken);
        }

        public async Task<CourseSubscriptionDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CourseSubscriptions.GetTableNoTracking()
                .Where(cs => cs.Id == id)
                  .Select(ToCourseSubscriptionDto).FirstAsync(cancellationToken);
        }

        public async Task<CourseSubscription> CreateAsync(CourseSubscription entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.CourseSubscriptions.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<CourseSubscription> UpdateAsync(CourseSubscription entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CourseSubscriptions.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.CourseSubscriptions.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.CourseSubscriptions.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }
        public async Task<IEnumerable<CourseSubscriptionDto>> GetByStudentIdAndStatusAsync
            (int studentId, string status, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CourseSubscriptions.GetTableNoTracking()
                .Where(cs => cs.Student.Id == studentId && cs.Status == status)
                  .Select(ToCourseSubscriptionDto)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<CourseSubscriptionDto>> GetByTeacherIdAsync(int teacherId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CourseSubscriptions.GetTableNoTracking()
                .Where(cs => cs.Course.Teacher.Id == teacherId)
                .Select(ToCourseSubscriptionDto)
                .ToListAsync(cancellationToken);
        }

        public Task<CourseSubscription?> GetByIdForEditAsync(int id, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.CourseSubscriptions.GetTableAsTracking()
                 .Include(cs => cs.Student)
                 .Include(cs => cs.Course)
                 .FirstOrDefaultAsync(cs => cs.Id == id, cancellationToken);
        }

        #region Convert CourseSubscription to CourseSubscriptionDto expression
        public static Expression<Func<CourseSubscription, CourseSubscriptionDto>> ToCourseSubscriptionDto =>
            cs => new CourseSubscriptionDto
            {
                StudentId = cs.StudentId,
                StudentName = cs.Student.User.FullName,
                CourseId = cs.CourseId,
                CourseName = cs.Course.Title,
                Status = cs.Status,
                CreatedAt = cs.CreatedAt,
                Lectures = cs.Course.Lectures.Select(l => new LectureDto
                {
                    Id = l.Id,
                    Title = l.Title,
                    Materials = l.Materials.Select(m => new MaterialDto
                    {
                        Id = m.Id,
                        Type = m.Type,
                        FileUrl = m.FileUrl,
                        IsFree = m.IsFree,
                    }).ToList()
                }).ToList()
            };
        #endregion

    }
}
