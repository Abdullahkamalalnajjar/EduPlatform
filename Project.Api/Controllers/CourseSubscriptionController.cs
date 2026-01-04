using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.CourseSubscriptions.Commands.Models;
using Project.Core.Features.CourseSubscriptions.Queries.Models;
using Project.Data.AppMetaData;

namespace Project.Api.Controllers
{
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
        [HttpGet(Router.CourseSubscriptionRouting.GetByStudentAndStatus)]
        public async Task<IActionResult> GetByStudentAndStatus(int studentId, string status)
        {
            var request = new GetCourseSubscriptionByStudentAndStatusQuery(studentId, status);
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

        [HttpPut(Router.CourseSubscriptionRouting.ChangeStatus)]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeCourseSubscriptionStatusCommand command)
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

        [HttpGet(Router.CourseSubscriptionRouting.List + "/teacher/{teacherId}")]
        public async Task<IActionResult> GetByTeacher(int teacherId)
        {
            var request = new GetStudentSubscriptionsByTeacherQuery { TeacherId = teacherId };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
