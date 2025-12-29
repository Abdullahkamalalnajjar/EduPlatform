using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Data.AppMetaData;
using Project.Core.Features.Exams.Commands.Models;
using Project.Core.Features.Exams.Queries.Models;

namespace Project.Api.Controllers
{
    [ApiController]
    public class QuestionController : AppBaseController
    {
        [HttpGet(Router.ExamRouting.List + "/questions")]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetAllQuestionsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.ExamRouting.GetById + "/questions")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetQuestionByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(Router.ExamRouting.Create + "/questions")]
        public async Task<IActionResult> Create([FromForm] CreateQuestionCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.ExamRouting.Edit + "/questions")]
        public async Task<IActionResult> Edit([FromForm] EditQuestionCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.ExamRouting.Delete + "/questions")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteQuestionCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
