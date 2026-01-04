using MediatR;
using Project.Core.Features.CourseSubscriptions.Queries.Models;
using Project.Data.Dtos;
using Project.Service.Abstracts;

namespace Project.Core.Features.CourseSubscriptions.Queries.Handlers
{
    public class GetStudentSubscriptionsByTeacherQueryHandler : ResponseHandler, IRequestHandler<GetStudentSubscriptionsByTeacherQuery, Response<IEnumerable<CourseSubscriptionDto>>>
    {
        private readonly ICourseSubscriptionService _subscriptionService;

        public GetStudentSubscriptionsByTeacherQueryHandler(ICourseSubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public async Task<Response<IEnumerable<CourseSubscriptionDto>>> Handle(GetStudentSubscriptionsByTeacherQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await _subscriptionService.GetByTeacherIdAsync(request.TeacherId, cancellationToken);
            
            if (!subscriptions.Any())
                return NotFound<IEnumerable<CourseSubscriptionDto>>("No student subscriptions found for this teacher");

            return Success(subscriptions);
        }
    }
}
