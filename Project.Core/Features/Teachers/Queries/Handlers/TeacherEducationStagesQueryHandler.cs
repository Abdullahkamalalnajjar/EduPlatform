using Project.Core.Features.Teachers.Queries.Models;
using Project.Core.Features.Teachers.Queries.Results;

namespace Project.Core.Features.Teachers.Queries.Handlers
{
    public class TeacherEducationStagesQueryHandler : ResponseHandler,
        IRequestHandler<GetTeacherEducationStagesQuery, Response<IEnumerable<TeacherByGradeSubjectResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherEducationStagesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<TeacherByGradeSubjectResponse>>> Handle(GetTeacherEducationStagesQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.TeacherEducationStages.GetTableNoTracking()
                .Include(ts => ts.EducationStage)
                .Include(ts => ts.Teacher)
                .ThenInclude(t => t.User)
                .Where(ts => ts.TeacherId == request.TeacherId)
                .ToListAsync(cancellationToken);

            var result = items.Select(i => new TeacherByGradeSubjectResponse
            {
                Id = i.TeacherId,
                UserId = i.Teacher.ApplicationUserId,
                FullName = i.Teacher.User.FullName,
                SubjectId = i.Teacher.SubjectId
            }).ToList();
            return Success<IEnumerable<TeacherByGradeSubjectResponse>>(result);
        }
    }
}
