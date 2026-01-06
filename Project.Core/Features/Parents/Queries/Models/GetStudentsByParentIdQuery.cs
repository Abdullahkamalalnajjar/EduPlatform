using MediatR;
using Project.Core.Bases;
using Project.Data.Dtos;

namespace Project.Core.Features.Parents.Queries.Models
{
    public class GetStudentsByParentIdQuery : IRequest<Response<IEnumerable<ParentStudentDto>>>
    {
        public int ParentId { get; set; }
    }
}
