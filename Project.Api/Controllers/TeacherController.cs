using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.Teachers.Queries.Models;
using Project.Data.AppMetaData;

namespace Project.Api.Controllers
{
    public class TeacherController : AppBaseController
    {

        [HttpGet(Router.TeacherRouting.List + "/by-education-stage/{educationStageId}/{subjectId}")]
        public async Task<IActionResult> GetByEducationStageAndSubject(int educationStageId, int subjectId)
        {
            var request = new GetTeachersByEducationStageSubjectQuery { EducationStageId = educationStageId, SubjectId = subjectId };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
