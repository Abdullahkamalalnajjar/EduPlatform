using Project.Core.Features.CourseSubscriptions.Commands.Models;
using Project.Data.Entities.Subscriptions;

namespace Project.Core.Features.CourseSubscriptions.Commands.Handlers
{
    public class CourseSubscriptionCommandHandler : ResponseHandler,
        IRequestHandler<CreateCourseSubscriptionCommand, Response<int>>,
        IRequestHandler<EditCourseSubscriptionCommand, Response<int>>,
        IRequestHandler<DeleteCourseSubscriptionCommand, Response<string>>,
        IRequestHandler<ChangeCourseSubscriptionStatusCommand, Response<int>>
    {
        private readonly ICourseSubscriptionService _service;
        private readonly IMapper _mapper;

        public CourseSubscriptionCommandHandler(ICourseSubscriptionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCourseSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var entity = new CourseSubscription { StudentId = request.StudentId, CourseId = request.CourseId };
            var created = await _service.CreateAsync(entity, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditCourseSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<int>("CourseSubscription not found");
            entity.StudentId = request.StudentId;
            entity.CourseId = request.CourseId;
            entity.Status = request.Status ?? entity.Status;
            var updated = await _service.UpdateAsync(entity, cancellationToken);
            return Success(updated.Id);
        }

        public async Task<Response<string>> Handle(DeleteCourseSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<string>("CourseSubscription not found");
            await _service.DeleteAsync(request.Id, cancellationToken);
            return Success("Deleted");
        }

        public async Task<Response<int>> Handle(ChangeCourseSubscriptionStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return NotFound<int>("CourseSubscription not found");
            entity.Status = request.Status ?? entity.Status;
            var updated = await _service.UpdateAsync(entity, cancellationToken);
            return Success(updated.Id);
        }
    }
}
