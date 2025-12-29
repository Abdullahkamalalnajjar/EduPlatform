using MediatR;
using Project.Core.Features.Subjects.Queries.Results;

namespace Project.Core.Features.Subjects.Queries.Models
{
    public class GetSubjectByIdQuery : IRequest<Response<SubjectResponse>>
    {
        public int Id { get; set; }
    }
}