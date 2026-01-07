using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.Subjects.Commands.Models;
using Project.Core.Features.Subjects.Queries.Models;
using Project.Core.Features.Subjects.Queries.Results;
using Project.Data.AppMetaData;

namespace Project.Api.Controllers
{
    public class SubjectController : AppBaseController
    {
        [HttpGet(Router.SubjectRouting.List)]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetAllSubjectsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.SubjectRouting.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetSubjectByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpGet(Router.SubjectRouting.GetById + "/teachers")]
        public async Task<IActionResult> GetTeachersWithCourses(int id)
        {
            var request = new GetTeacherWithCourseBySubjectIdQuery { SubjectId = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(Router.SubjectRouting.Create)]
        public async Task<IActionResult> Create([FromForm] CreateSubjectCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.SubjectRouting.Edit)]
        public async Task<IActionResult> Edit([FromForm] EditSubjectCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.SubjectRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteSubjectCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        // Optional: simple paginated endpoint that uses query parameters page/size
        [HttpGet(Router.SubjectRouting.Paginated)]
        public async Task<IActionResult> Paginated(int page = 1, int size = 10)
        {
            var response = await Mediator.Send(new GetAllSubjectsQuery());
            if (response is null || !response.Succeeded || response.Data is null)
                return NewResult(response!);

            var data = response.Data.Skip((page - 1) * size).Take(size).ToList();
            var pagedResponse = new Project.Core.Bases.Response<IEnumerable<SubjectResponse>>(data, "Paginated subjects")
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
            return NewResult(pagedResponse);
        }
    }
}
