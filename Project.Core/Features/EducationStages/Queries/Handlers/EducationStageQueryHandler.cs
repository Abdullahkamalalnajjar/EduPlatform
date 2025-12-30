using MediatR;
using Project.Core.Features.EducationStages.Queries.Models;
using Project.Core.Features.EducationStages.Queries.Results;
using Project.Service.Abstracts;

namespace Project.Core.Features.EducationStages.Queries.Handlers
{
    public class EducationStageQueryHandler : ResponseHandler,
        IRequestHandler<GetAllEducationStagesQuery, Response<IEnumerable<EducationStageResponse>>>,
        IRequestHandler<GetEducationStageByIdQuery, Response<EducationStageResponse>>
    {
        private readonly IEducationStageService _service;

        public EducationStageQueryHandler(IEducationStageService service)
        {
            _service = service;
        }

        public async Task<Response<IEnumerable<EducationStageResponse>>> Handle(GetAllEducationStagesQuery request, CancellationToken cancellationToken)
        {
            var items = await _service.GetAllAsync(cancellationToken);
            var result = items.Select(e => new EducationStageResponse { Id = e.Id, Name = e.Name }).ToList();
            return Success<IEnumerable<EducationStageResponse>>(result);
        }

        public async Task<Response<EducationStageResponse>> Handle(GetEducationStageByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _service.GetByIdAsync(request.Id, cancellationToken);
            if (item is null) return NotFound<EducationStageResponse>("Education stage not found");
            var resp = new EducationStageResponse { Id = item.Id, Name = item.Name };
            return Success(resp);
        }
    }
}
