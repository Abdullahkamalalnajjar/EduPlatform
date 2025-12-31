using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.Lectures.Commands.Models;
using Project.Core.Features.Lectures.Queries.Models;
using Project.Data.AppMetaData;
using Project.Service.Abstracts;

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

        // Accept multipart/form-data for file uploads or a URL for videos
        [HttpPost(Router.LectureRouting.Create + "/materials")]
        public async Task<IActionResult> CreateMaterial([FromForm] string type, [FromForm] int lectureId, IFormFile? file, [FromForm] string? videoUrl, [FromForm] bool isFree = false)
        {
            // Video type expects a URL
            if (string.Equals(type, "video", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(videoUrl))
                {
                    return BadRequest(new { Succeeded = false, Message = "Video materials require a fileUrl (video URL)." });
                }

                var cmd = new CreateLectureMaterialCommand { Type = type, VideoUrl = videoUrl, LectureId = lectureId, IsFree = isFree };
                var response = await Mediator.Send(cmd);
                return NewResult(response);
            }

            // For images or pdfs require a file upload
            if (file is null)
            {
                return BadRequest(new { Succeeded = false, Message = "File is required for non-video material types." });
            }

            var fileService = HttpContext.RequestServices.GetService<IFileService>();
            if (fileService is null)
            {
                return BadRequest(new { Succeeded = false, Message = "File service not available." });
            }

            // choose location folder name
            var location = "uploads/lectures";
            var uploadedUrl = await fileService.UploadFile(location, file);
            if (string.IsNullOrEmpty(uploadedUrl) || uploadedUrl == "FailedToUploadImage" || uploadedUrl == "NoImage" || uploadedUrl == "InvalidFileType")
            {
                return BadRequest(new { Succeeded = false, Message = "Failed to upload file or invalid file type." });
            }

            var command = new CreateLectureMaterialCommand { Type = type, VideoUrl = uploadedUrl, LectureId = lectureId, IsFree = isFree };
            var resp = await Mediator.Send(command);
            return NewResult(resp);
        }

        [HttpPut(Router.LectureRouting.Edit + "/materials")]
        public async Task<IActionResult> EditMaterial([FromBody] EditLectureMaterialCommand command)
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
