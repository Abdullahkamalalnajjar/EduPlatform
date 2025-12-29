using AutoMapper;
using MediatR;
using Project.Core.Features.CourseSubscriptions.Queries.Models;
using Project.Core.Features.CourseSubscriptions.Queries.Results;
using Project.Service.Abstracts;

namespace Project.Core.Features.CourseSubscriptions.Queries.Handlers
{
    public class CourseSubscriptionQueryHandler : ResponseHandler,
        IRequestHandler<GetAllCourseSubscriptionsQuery, Response<IEnumerable<CourseSubscriptionResponse>>>,
        IRequestHandler<GetCourseSubscriptionByIdQuery, Response<CourseSubscriptionResponse>>
    {
        private readonly ICourseSubscriptionService _service;
        private readonly IMapper _mapper;

        public CourseSubscriptionQueryHandler(ICourseSubscriptionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CourseSubscriptionResponse>>> Handle(GetAllCourseSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var items = await _service.GetAllAsync(cancellationToken);
            var result = items.Select(i => new CourseSubscriptionResponse { Id = i.Id, StudentId = i.StudentId, CourseId = i.CourseId, Status = i.Status, CreatedAt = i.CreatedAt }).ToList();
            return Success<IEnumerable<CourseSubscriptionResponse>>(result);
        }

        public async Task<Response<CourseSubscriptionResponse>> Handle(GetCourseSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (item is null) return NotFound<CourseSubscriptionResponse>("CourseSubscription not found");
            var resp = new CourseSubscriptionResponse { Id = item.Id, StudentId = item.StudentId, CourseId = item.CourseId, Status = item.Status, CreatedAt = item.CreatedAt };
            return Success(resp);
        }
    }
}
