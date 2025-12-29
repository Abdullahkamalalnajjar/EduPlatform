using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Data.AppMetaData;
using Project.Core.Features.Exams.Commands.Models;
using Project.Core.Features.Exams.Queries.Models;

namespace Project.Api.Controllers
{
    [ApiController]
    public class QuestionOptionController : AppBaseController
    {
        [HttpGet(Router.ExamRouting.List + "/questions/options")]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetAllQuestionOptionsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.ExamRouting.GetById + "/questions/options")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetQuestionOptionByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(Router.ExamRouting.Create + "/questions/options")]
        public async Task<IActionResult> Create([FromBody] CreateQuestionOptionCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.ExamRouting.Edit + "/questions/options")]
        public async Task<IActionResult> Edit([FromBody] EditQuestionOptionCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.ExamRouting.Delete + "/questions/options")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteQuestionOptionCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
