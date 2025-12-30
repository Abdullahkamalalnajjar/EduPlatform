using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Data.AppMetaData;
using Project.Core.Features.Students.Queries.Models;

namespace Project.Api.Controllers
{
    [ApiController]
    public class StudentController : AppBaseController
    {
        [HttpGet(Router.StudentRouting.List + "/grade/{gradeYear}")]
        public async Task<IActionResult> GetByGrade(int gradeYear)
        {
            var request = new GetStudentsByGradeQuery { GradeYear = gradeYear };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
