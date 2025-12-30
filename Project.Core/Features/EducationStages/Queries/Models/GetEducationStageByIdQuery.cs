using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.EducationStages.Queries.Models
{
    public class GetEducationStageByIdQuery : IRequest<Response<Project.Core.Features.EducationStages.Queries.Results.EducationStageResponse>>
    {
        public int Id { get; set; }
    }
}
