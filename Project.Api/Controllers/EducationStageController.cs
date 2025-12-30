using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.EducationStages.Queries.Models;
using Project.Data.AppMetaData;

namespace Project.Api.Controllers
{
    public class EducationStageController : AppBaseController
    {
        [HttpGet(Router.EducationStageRouting.List)]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetAllEducationStagesQuery());
            return NewResult(response);
        }

        [HttpGet(Router.EducationStageRouting.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetEducationStageByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
