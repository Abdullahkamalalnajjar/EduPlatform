using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.EducationStages.Queries.Models
{
    public class GetAllEducationStagesQuery : IRequest<Response<IEnumerable<Project.Core.Features.EducationStages.Queries.Results.EducationStageResponse>>> { }
}
