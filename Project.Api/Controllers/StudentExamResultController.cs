using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.StudentExamResults.Commands.Models;
using Project.Core.Features.StudentExamResults.Queries.Models;
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

        [HttpPost(Router.ExamRouting.Create + "/results")]
        public async Task<IActionResult> Create([FromBody] CreateStudentExamResultCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
