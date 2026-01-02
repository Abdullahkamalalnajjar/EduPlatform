using AutoMapper;
using MediatR;
using Project.Core.Features.Lectures.Queries.Models;
using Project.Core.Features.Lectures.Queries.Results;
using Project.Data.Interfaces;

namespace Project.Core.Features.Lectures.Queries.Handlers
{
    public class LectureMaterialQueryHandler : ResponseHandler,
        IRequestHandler<GetAllLectureMaterialsQuery, Response<IEnumerable<LectureMaterialResponse>>>,
        IRequestHandler<GetLectureMaterialByIdQuery, Response<LectureMaterialResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LectureMaterialQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<LectureMaterialResponse>>> Handle(GetAllLectureMaterialsQuery request, CancellationToken cancellationToken)
        {
            var materials = await _unitOfWork.LectureMaterials.GetTableNoTracking().ToListAsync(cancellationToken);
            var result = materials.Select(m => new LectureMaterialResponse { Id = m.Id,Title=m.Title ,Type = m.Type, FileUrl = m.FileUrl ,LectureId = m.LectureId, IsFree = m.IsFree }).ToList();
            return Success<IEnumerable<LectureMaterialResponse>>(result);
        }

        public async Task<Response<LectureMaterialResponse>> Handle(GetLectureMaterialByIdQuery request, CancellationToken cancellationToken)
        {
            var m = await _unitOfWork.LectureMaterials.GetByIdAsync(request.Id);
            if (m is null) return NotFound<LectureMaterialResponse>("Material not found");
            var resp = new LectureMaterialResponse { Id = m.Id, Title = m.Title, Type = m.Type, FileUrl = m.FileUrl, LectureId = m.LectureId, IsFree = m.IsFree };
            return Success(resp);
        } 
    }
}