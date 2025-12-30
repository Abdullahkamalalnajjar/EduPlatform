using Project.Core.Features.Lectures.Commands.Models;
using Project.Data.Entities.Content;

namespace Project.Core.Features.Lectures.Commands.Handlers
{
    public class LectureMaterialCommandHandler : ResponseHandler,
        IRequestHandler<CreateLectureMaterialCommand, Response<int>>,
        IRequestHandler<EditLectureMaterialCommand, Response<int>>,
        IRequestHandler<DeleteLectureMaterialCommand, Response<string>>,
        IRequestHandler<ChangeIsFreeLectureMaterialCommand, Response<int>>
    {
        private readonly ILectureMaterialService _lectureMaterialService;
        private readonly IMapper _mapper;

        public LectureMaterialCommandHandler(ILectureMaterialService lectureMaterialService, IMapper mapper)
        {
            _lectureMaterialService = lectureMaterialService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateLectureMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = new LectureMaterial { Type = request.Type, FileUrl = request.FileUrl, LectureId = request.LectureId };
            var created = await _lectureMaterialService.CreateAsync(material, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditLectureMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = await _lectureMaterialService.GetByIdAsync(request.Id, cancellationToken);
            if (material is null) return NotFound<int>("Material not found");
            material.Type = request.Type;
            material.FileUrl = request.FileUrl;
            material.LectureId = request.LectureId;
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

        public async Task<Response<int>> Handle(ChangeIsFreeLectureMaterialCommand request, CancellationToken cancellationToken)
        {
            var result = await _lectureMaterialService.ChangeIsFreeLectureMaterialAsync(request.LectureMaterialId, request.IsFree, cancellationToken);
            return Success(request.LectureMaterialId);
        }
    }
}