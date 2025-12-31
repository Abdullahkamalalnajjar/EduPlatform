using Project.Core.Features.Teachers.Queries.Models;
using Project.Core.Features.Teachers.Queries.Results;
using Project.Service.Abstracts;

namespace Project.Core.Features.Teachers.Queries.Handlers
{
    public class TeacherQueryHandler : ResponseHandler,
        IRequestHandler<GetTeachersByGradeSubjectQuery, Response<IEnumerable<TeacherByGradeSubjectResponse>>>
    {
        private readonly ITeacherService _teacherService;

        public TeacherQueryHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<Response<IEnumerable<TeacherByGradeSubjectResponse>>> Handle(GetTeachersByGradeSubjectQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _teacherService.GetByGradeYearAndSubjectAsync(request.GradeYear, request.SubjectId, cancellationToken);
            var result = teachers.Select(t => new TeacherByGradeSubjectResponse { Id = t.Id, UserId = t.ApplicationUserId, ApplicationUserId = t.ApplicationUserId, FullName = t.User.FullName, SubjectId = t.SubjectId }).ToList();
            return Success<IEnumerable<TeacherByGradeSubjectResponse>>(result);
        }
    }
}
