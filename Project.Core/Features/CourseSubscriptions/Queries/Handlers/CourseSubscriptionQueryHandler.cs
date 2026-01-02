using Project.Core.Features.CourseSubscriptions.Queries.Models;

namespace Project.Core.Features.CourseSubscriptions.Queries.Handlers
{
    public class CourseSubscriptionQueryHandler : ResponseHandler,
        IRequestHandler<GetAllCourseSubscriptionsQuery, Response<IEnumerable<CourseSubscriptionDto>>>,
        IRequestHandler<GetCourseSubscriptionByIdQuery, Response<CourseSubscriptionDto>>,
        IRequestHandler<GetCourseSubscriptionByStudentAndStatusQuery, Response<IEnumerable<CourseSubscriptionDto>>>
    {
        private readonly ICourseSubscriptionService _service;
        private readonly IMapper _mapper;

        public CourseSubscriptionQueryHandler(ICourseSubscriptionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CourseSubscriptionDto>>> Handle(GetAllCourseSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var items = await _service.GetAllAsync(cancellationToken);
            return Success<IEnumerable<CourseSubscriptionDto>>(items);
        }

        public async Task<Response<CourseSubscriptionDto>> Handle(GetCourseSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (item is null) return NotFound<CourseSubscriptionDto>("CourseSubscription not found");
            return Success(item);
        }
        public async Task<Response<IEnumerable<CourseSubscriptionDto>>> Handle(GetCourseSubscriptionByStudentAndStatusQuery request, CancellationToken cancellationToken)
        {
            var courseSubscriptions = await _service.GetByStudentIdAndStatusAsync(request.StudentId, request.Status, cancellationToken);
            return Success<IEnumerable<CourseSubscriptionDto>>(courseSubscriptions);
        }
    }
}
