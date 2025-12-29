using MediatR;
using Project.Core.Bases;
using System.Collections.Generic;

namespace Project.Core.Features.CourseSubscriptions.Queries.Models
{
    public class GetAllCourseSubscriptionsQuery : IRequest<Response<IEnumerable<Project.Core.Features.CourseSubscriptions.Queries.Results.CourseSubscriptionResponse>>> { }
}
