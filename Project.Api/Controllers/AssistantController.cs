using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.Assistants.Queries.Models;

namespace Project.Api.Controllers
{
    public class AssistantController : AppBaseController
    {
        [HttpGet("ByTeacher/{teacherId}")]
        public async Task<IActionResult> GetByTeacherId(int teacherId)
        {
            var request = new GetAssistantsByTeacherIdQuery { TeacherId = teacherId };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
