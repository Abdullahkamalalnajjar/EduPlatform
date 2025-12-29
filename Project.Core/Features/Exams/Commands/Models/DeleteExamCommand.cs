using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Commands.Models
{
    public class DeleteExamCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
