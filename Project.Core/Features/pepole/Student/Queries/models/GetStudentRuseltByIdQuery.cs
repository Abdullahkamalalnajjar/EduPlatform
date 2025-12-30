using MediatR;
using Project.Core.Features.pepole.Student.Queries.Ruselt;

namespace Project.Core.Features.pepole.Student.Queries.Models
{
    public class GetStudentRuseltByIdQuery : IRequest<StudentRuselt>
    {
        public int Id { get; set; }
        public GetStudentRuseltByIdQuery(int id) => Id = id;
    }
}