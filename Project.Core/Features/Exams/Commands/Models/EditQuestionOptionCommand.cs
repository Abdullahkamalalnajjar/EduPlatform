using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Commands.Models
{
    public class EditQuestionOptionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
