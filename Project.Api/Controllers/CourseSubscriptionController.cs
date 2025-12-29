using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Data.AppMetaData;
using Project.Core.Features.CourseSubscriptions.Commands.Models;
using Project.Core.Features.CourseSubscriptions.Queries.Models;

namespace Project.Api.Controllers
{
    [ApiController]
    public class CourseSubscriptionController : AppBaseController
    {
        [HttpGet(Router.CourseSubscriptionRouting.List)]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetAllCourseSubscriptionsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.CourseSubscriptionRouting.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetCourseSubscriptionByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(Router.CourseSubscriptionRouting.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCourseSubscriptionCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.CourseSubscriptionRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditCourseSubscriptionCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.CourseSubscriptionRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteCourseSubscriptionCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
