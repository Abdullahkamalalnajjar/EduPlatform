using Project.Core.Features.Lectures.Commands.Models;
using Project.Data.Entities.Content;

namespace Project.Core.Features.Lectures.Commands.Handlers
{
    public class LectureMaterialCommandHandler : ResponseHandler,
        IRequestHandler<CreateLectureMaterialCommand, Response<int>>,
        IRequestHandler<EditLectureMaterialCommand, Response<int>>,
        IRequestHandler<DeleteLectureMaterialCommand, Response<string>>
    {
        private readonly ILectureMaterialService _lectureMaterialService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public LectureMaterialCommandHandler(ILectureMaterialService lectureMaterialService, IMapper mapper, IFileService fileService)
        {
            _lectureMaterialService = lectureMaterialService;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Response<int>> Handle(CreateLectureMaterialCommand request, CancellationToken cancellationToken)
        {
            string fileUrl = request.VideoUrl;

            // If material is not a video and a file is provided, upload it
            if (!string.Equals(request.Type, "video", StringComparison.OrdinalIgnoreCase) && request.File is not null)
            {
                // reuse existing UploadImage method for saving pdf or image
                var location = "uploads/lectures";
                var uploadResult = await _fileService.UploadImage(location, request.File);
                if (string.IsNullOrEmpty(uploadResult) || uploadResult == "FailedToUploadImage" || uploadResult == "NoImage")
                {
                    return BadRequest<int>("Failed to upload file");
                }

                fileUrl = uploadResult;
            }

            var material = new LectureMaterial { Type = request.Type,Title=request.Title ,FileUrl = fileUrl, LectureId = request.LectureId, IsFree = request.IsFree };
            var created = await _lectureMaterialService.CreateAsync(material, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditLectureMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = await _lectureMaterialService.GetByIdAsync(request.Id, cancellationToken);
            if (material is null) return NotFound<int>("Material not found");
            material.Type = request.Type;
            material.Title = request.Title;
            material.FileUrl = request.FileUrl;
            material.LectureId = request.LectureId;
            material.IsFree = request.IsFree;
            var updated = await _lectureMaterialService.UpdateAsync(material, cancellationToken);
            return Success(updated.Id);
        }

        public async Task<Response<string>> Handle(DeleteLectureMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = await _lectureMaterialService.GetByIdAsync(request.Id, cancellationToken);
            if (material is null) return NotFound<string>("Material not found");
            await _lectureMaterialService.DeleteAsync(request.Id, cancellationToken);
            return Success("Deleted");
        }
    }
}