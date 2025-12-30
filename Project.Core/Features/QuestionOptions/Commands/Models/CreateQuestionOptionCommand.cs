using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.QuestionOptions.Commands.Models
{
    public class CreateQuestionOptionCommand : IRequest<Response<int>>
    {
        public string Content { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
