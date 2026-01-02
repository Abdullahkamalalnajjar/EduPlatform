using Project.Data.Entities.Curriculum;

namespace Project.Service.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Subjects.GetTableNoTracking()
                .Include(s => s.Teachers)
                .ToListAsync(cancellationToken);
        }

        public async Task<Subject?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Subjects.GetTableNoTracking()
                .Include(s => s.Teachers)
                .SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Subject> CreateAsync(Subject entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Subjects.AddAsync(entity, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task<Subject> UpdateAsync(Subject entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Subjects.Update(entity);
            await _unitOfWork.CompeleteAsync();
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (entity != null)
            {
                await _unitOfWork.Subjects.Delete(entity);
                await _unitOfWork.CompeleteAsync();
            }
        }

        public async Task<IEnumerable<SubjectDto>> GetTeacherWithCourseBySubjectId(int subjectId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Subjects.GetTableNoTracking().Where(x => x.Id == subjectId)
                .Select(s => new SubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    SubjectImageUrl = s.SubjectImageUrl,
                    Teachers = s.Teachers.Select(t => new TeacherDto
                    {
                        Id = t.Id,
                        TeacherName = t.User.FullName,
                        SubjectName = t.Subject.Name,
                        PhoneNumber = t.PhoneNumber,
                        FacebookUrl = t.FacebookUrl,
                        TelegramUrl = t.TelegramUrl,
                        WhatsAppNumber = t.WhatsAppNumber,
                        PhotoUrl = t.PhotoUrl,
                        TeacherEducationStages = t.TeacherEducationStages.Select(tes => new TeacherEducationStageDto
                        {
                            Id = tes.Id,
                            EducationStageName = tes.EducationStage.Name
                        }).ToList(),
                        Courses = t.Courses.Select(c => new CourseDto
                        {
                            Id = c.Id,
                            Title = c.Title,
                            EducationStageId = c.EducationStageId,
                            EducationStageName = c.EducationStage.Name,
                            TeacherId = c.TeacherId,
                            TeacherName = t.User.FullName,
                            Lectures = c.Lectures.Select(l => new LectureDto
                            {
                                Id = l.Id,
                                Title = l.Title,
                                Materials = l.Materials.Select(m => new MaterialDto
                                {
                                    Id = m.Id,
                                    Title = m.Title,
                                    Type = m.Type,
                                    FileUrl = m.FileUrl,
                                    IsFree = m.IsFree
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToListAsync(cancellationToken);
        }
    }
}