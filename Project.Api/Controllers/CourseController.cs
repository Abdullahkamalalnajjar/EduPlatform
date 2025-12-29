using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Data.AppMetaData;
using Project.Core.Features.Courses.Commands.Models;
using Project.Core.Features.Courses.Queries.Models;
using Project.Core.Features.Courses.Queries.Results;

namespace Project.Api.Controllers
{
    [ApiController]
    public class CourseController : AppBaseController
    {
        [HttpGet(Router.CourseRouting.List)]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetAllCoursesQuery());
            return NewResult(response);
        }

        [HttpGet(Router.CourseRouting.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetCourseByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(Router.CourseRouting.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.CourseRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditCourseCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.CourseRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteCourseCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}