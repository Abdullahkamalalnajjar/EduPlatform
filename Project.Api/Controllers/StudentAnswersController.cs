using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.StudentAnswers.Commands.Models;
using Project.Core.Features.StudentAnswers.Queries.Models;
using Project.Data.AppMetaData;

namespace Project.Api.Controllers
{
    public class StudentAnswersController : AppBaseController
    {
        [HttpPost(Router.StudentAnswersRouting.SubmitImageAnswer)]
        public async Task<IActionResult> SubmitImageAnswer([FromForm] SubmitImageAnswerCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.StudentAnswersRouting.GetTemporaryAnswers)]
        public async Task<IActionResult> GetTemporaryAnswers(int examId, int studentId)
        {
            var request = new GetTemporaryAnswersByExamQuery { ExamId = examId, StudentId = studentId };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
