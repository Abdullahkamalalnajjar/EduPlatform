using Project.Core.Features.Students.Queries.Models;
using Project.Core.Features.Students.Queries.Results;
using Project.Service.Abstracts;

namespace Project.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
        IRequestHandler<GetStudentsByGradeQuery, Response<IEnumerable<StudentByGradeResponse>>>
    {
        private readonly IStudentService _studentService;

        public StudentQueryHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<Response<IEnumerable<StudentByGradeResponse>>> Handle(GetStudentsByGradeQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetByGradeYearAsync(request.GradeYear, cancellationToken);
            var result = students.Select(s => new StudentByGradeResponse { Id = s.Id, GradeYear = s.GradeYear, UserId = s.ApplicationUserId, FullName = s.User.FullName }).ToList();
            return Success<IEnumerable<StudentByGradeResponse>>(result);
        }
    }
}
