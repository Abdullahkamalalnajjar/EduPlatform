using MediatR;
using Project.Core.Features.Exams.Queries.Models;
using Project.Service.Abstracts;

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
                return NotFound<IEnumerable<StudentExamSubmissionDto>>("?? ???? ???????? ????? ???? ????????");

            return Success<IEnumerable<StudentExamSubmissionDto>>(submissions);
        }
    }
}
