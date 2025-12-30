using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.QuestionOptions.Commands.Models;
using Project.Core.Features.QuestionOptions.Queries.Models;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("api/v1/question-options")]
    public class QuestionOptionController : AppBaseController
    {
        //[HttpGet]
        //public async Task<IActionResult> List()
        //{
        //    var response = await Mediator.Send(new GetAllQuestionOptionsQuery());
        //    return NewResult(response);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetQuestionOptionByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQuestionOptionCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditQuestionOptionCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteQuestionOptionCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
