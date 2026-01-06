using Project.Core.Features.Exams.Queries.Models;

namespace Project.Core.Features.Exams.Queries.Handlers
{
    public class GetStudentsByLectureExamQueryHandler : ResponseHandler, IRequestHandler<GetStudentsByLectureExamQuery, Response<IEnumerable<StudentExamSubmissionDto>>>
    {
        private readonly IExamService _examService;

        public GetStudentsByLectureExamQueryHandler(IExamService examService)
        {
            _examService = examService;
        }

        public async Task<Response<IEnumerable<StudentExamSubmissionDto>>> Handle(GetStudentsByLectureExamQuery request, CancellationToken cancellationToken)
        {
            var submissions = await _examService.GetStudentSubmissionsByLectureAsync(request.LectureId, cancellationToken);

            if (!submissions.Any())
                return NotFound<IEnumerable<StudentExamSubmissionDto>>("لا يوجد تسليمات");

            return Success<IEnumerable<StudentExamSubmissionDto>>(submissions);
        }
    }
}
