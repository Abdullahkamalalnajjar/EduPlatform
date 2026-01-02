using Project.Core.Features.Subjects.Queries.Models;
using Project.Core.Features.Subjects.Queries.Results;

namespace Project.Core.Features.Subjects.Queries.Handlers
{
    public class SubjectQueryHandler : ResponseHandler,
        IRequestHandler<GetAllSubjectsQuery, Response<IEnumerable<SubjectResponse>>>,
        IRequestHandler<GetSubjectByIdQuery, Response<SubjectResponse>>,
        IRequestHandler<GetTeacherWithCourseBySubjectIdQuery, Response<IEnumerable<SubjectDto>>>
    {
        private readonly ISubjectService _subjectService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectQueryHandler(ISubjectService subjectService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _subjectService = subjectService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<SubjectResponse>>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _unitOfWork.Subjects.GetTableNoTracking().ToListAsync(cancellationToken);
            var result = subjects.Select(s => new SubjectResponse { Id = s.Id, Name = s.Name, SubjectImageUrl = s.SubjectImageUrl }).ToList();
            return Success<IEnumerable<SubjectResponse>>(result);
        }

        public async Task<Response<SubjectResponse>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(request.Id);
            if (subject is null) return NotFound<SubjectResponse>("Subject not found");
            var resp = new SubjectResponse { Id = subject.Id, Name = subject.Name };
            return Success(resp);
        }

        public async Task<Response<IEnumerable<SubjectDto>>> Handle(GetTeacherWithCourseBySubjectIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _subjectService.GetTeacherWithCourseBySubjectId(request.SubjectId, cancellationToken);
            if (result is null || !result.Any())
                return NotFound<IEnumerable<SubjectDto>>("Subject or teachers not found");
            return Success(result);
        }
    }
}