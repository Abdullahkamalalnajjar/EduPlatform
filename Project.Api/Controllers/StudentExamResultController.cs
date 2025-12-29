using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.Exams.Commands.Models;
using Project.Core.Features.Exams.Queries.Models;
using Project.Data.AppMetaData;

namespace Project.Api.Controllers
{
    public class StudentExamResultController : AppBaseController
    {
        [HttpGet(Router.ExamRouting.List + "/results")]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetAllStudentExamResultsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.ExamRouting.GetById + "/results")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetStudentExamResultByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(Router.ExamRouting.Create + "/results")]
        public async Task<IActionResult> Create([FromBody] CreateStudentExamResultCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.ExamRouting.Edit + "/results")]
        public async Task<IActionResult> Edit([FromBody] EditStudentExamResultCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.ExamRouting.Delete + "/results")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteStudentExamResultCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
