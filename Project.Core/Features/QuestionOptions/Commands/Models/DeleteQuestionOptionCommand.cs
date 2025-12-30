using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.QuestionOptions.Commands.Models
{
    public class DeleteQuestionOptionCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
