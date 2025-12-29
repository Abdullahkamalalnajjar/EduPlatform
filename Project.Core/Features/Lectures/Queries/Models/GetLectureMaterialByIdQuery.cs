using MediatR;
using Project.Core.Features.Lectures.Queries.Results;

namespace Project.Core.Features.Lectures.Queries.Models
{
    public class GetLectureMaterialByIdQuery : IRequest<Response<LectureMaterialResponse>>
    {
        public int Id { get; set; }
    }
}