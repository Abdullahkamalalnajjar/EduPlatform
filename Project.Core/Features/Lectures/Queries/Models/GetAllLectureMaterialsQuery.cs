using MediatR;
using Project.Core.Features.Lectures.Queries.Results;

namespace Project.Core.Features.Lectures.Queries.Models
{
    public class GetAllLectureMaterialsQuery : IRequest<Response<IEnumerable<LectureMaterialResponse>>>
    {
    }
}