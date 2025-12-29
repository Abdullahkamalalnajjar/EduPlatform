using Project.Core.Features.Subjects.Queries.Models;
using Project.Core.Features.Subjects.Queries.Results;

namespace Project.Core.Features.Subjects.Queries.Handlers
{
    public class SubjectQueryHandler : ResponseHandler,
        IRequestHandler<GetAllSubjectsQuery, Response<IEnumerable<SubjectResponse>>>,
        IRequestHandler<GetSubjectByIdQuery, Response<SubjectResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<SubjectResponse>>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _unitOfWork.Subjects.GetTableNoTracking().ToListAsync(cancellationToken);
            var result = subjects.Select(s => new SubjectResponse { Id = s.Id, Name = s.Name }).ToList();
            return Success<IEnumerable<SubjectResponse>>(result);
        }

        public async Task<Response<SubjectResponse>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(request.Id);
            if (subject is null) return NotFound<SubjectResponse>("Subject not found");
            var resp = new SubjectResponse { Id = subject.Id, Name = subject.Name };
            return Success(resp);
        }
    }
}