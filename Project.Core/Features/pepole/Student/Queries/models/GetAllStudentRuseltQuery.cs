using MediatR;
using Project.Core.Features.pepole.Student.Queries.Ruselt;

namespace Project.Core.Features.pepole.Student.Queries.Models
{
    public class GetAllStudentRuseltQuery : IRequest<IEnumerable<StudentRuselt>>
    {
    }
}