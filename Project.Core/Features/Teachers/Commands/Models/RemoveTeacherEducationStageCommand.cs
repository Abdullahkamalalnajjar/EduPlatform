using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Teachers.Commands.Models
{
    public class RemoveTeacherEducationStageCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
