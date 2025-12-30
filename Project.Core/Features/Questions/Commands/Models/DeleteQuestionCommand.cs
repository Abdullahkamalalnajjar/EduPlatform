using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Questions.Commands.Models
{
    public class DeleteQuestionCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
