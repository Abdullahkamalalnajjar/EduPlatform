using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Data.AppMetaData;
using Project.Core.Features.Exams.Commands.Models;
using Project.Core.Features.Exams.Queries.Models;

namespace Project.Api.Controllers
{
    [ApiController]
    public class ExamController : AppBaseController
    {
        [HttpGet(Router.ExamRouting.List)]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetAllExamsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.ExamRouting.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetExamByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(Router.ExamRouting.Create)]
        public async Task<IActionResult> Create([FromBody] CreateExamCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.ExamRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditExamCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.ExamRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteExamCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
