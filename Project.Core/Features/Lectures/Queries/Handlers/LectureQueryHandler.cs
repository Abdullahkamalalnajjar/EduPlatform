using Project.Core.Features.Lectures.Queries.Models;
using Project.Core.Features.Lectures.Queries.Results;

namespace Project.Core.Features.Lectures.Queries.Handlers
{
    public class LectureQueryHandler : ResponseHandler,
        IRequestHandler<GetAllLecturesQuery, Response<IEnumerable<LectureResponse>>>,
        IRequestHandler<GetLectureByIdQuery, Response<LectureResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LectureQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<LectureResponse>>> Handle(GetAllLecturesQuery request, CancellationToken cancellationToken)
        {
            var lectures = await _unitOfWork.Lectures.GetTableNoTracking().ToListAsync(cancellationToken);
            var result = lectures.Select(l => new LectureResponse { Id = l.Id, Title = l.Title, CourseId = l.CourseId }).ToList();
            return Success<IEnumerable<LectureResponse>>(result);
        }

        public async Task<Response<LectureResponse>> Handle(GetLectureByIdQuery request, CancellationToken cancellationToken)
        {
            var lecture = await _unitOfWork.Lectures.GetByIdAsync(request.Id);
            if (lecture is null) return NotFound<LectureResponse>("Lecture not found");
            var resp = new LectureResponse { Id = lecture.Id, Title = lecture.Title, CourseId = lecture.CourseId };
            return Success(resp);
        }
    }
}