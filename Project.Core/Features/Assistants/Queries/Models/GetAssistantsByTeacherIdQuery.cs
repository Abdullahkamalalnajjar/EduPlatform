using MediatR;
using Project.Core.Bases;
using Project.Data.Dtos;

namespace Project.Core.Features.Assistants.Queries.Models
{
    public class GetAssistantsByTeacherIdQuery : IRequest<Response<IEnumerable<AssistantDto>>>
    {
        public int TeacherId { get; set; }
    }
}
