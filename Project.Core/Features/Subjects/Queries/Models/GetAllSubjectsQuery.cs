using Project.Core.Features.Subjects.Queries.Results;

namespace Project.Core.Features.Subjects.Queries.Models
{
    public class GetAllSubjectsQuery : IRequest<Response<IEnumerable<SubjectResponse>>>
    {
    }
}