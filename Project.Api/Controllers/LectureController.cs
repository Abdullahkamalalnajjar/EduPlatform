using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.Lectures.Commands.Models;
using Project.Core.Features.Lectures.Queries.Models;
using Project.Data.AppMetaData;

namespace Project.Api.Controllers
{
    public class LectureController : AppBaseController
    {
        [HttpGet(Router.LectureRouting.List)]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetAllLecturesQuery());
            return NewResult(response);
        }

        [HttpGet(Router.LectureRouting.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetLectureByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(Router.LectureRouting.Create)]
        public async Task<IActionResult> Create([FromBody] CreateLectureCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.LectureRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditLectureCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.LectureRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteLectureCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        // Lecture Materials endpoints
        [HttpGet(Router.LectureRouting.List + "/materials")]
        public async Task<IActionResult> Materials()
        {
            var response = await Mediator.Send(new GetAllLectureMaterialsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.LectureRouting.GetById + "/materials")]
        public async Task<IActionResult> GetMaterial(int id)
        {
            var request = new GetLectureMaterialByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(Router.LectureRouting.Create + "/materials")]
        public async Task<IActionResult> CreateMaterial([FromBody] CreateLectureMaterialCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.LectureRouting.Edit + "/materials")]
        public async Task<IActionResult> EditMaterial([FromBody] EditLectureMaterialCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.LectureRouting.Edit + "/materials/isfree")]
        public async Task<IActionResult> EditMaterialIsFree([FromBody] ChangeIsFreeLectureMaterialCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.LectureRouting.Delete + "/materials")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var request = new DeleteLectureMaterialCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

    }
}
