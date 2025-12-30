using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Questions.Queries.Models
{
    public class GetQuestionByIdQuery : IRequest<Response<Project.Core.Features.Questions.Queries.Results.QuestionResponse>>
    {
        public int Id { get; set; }
    }
}
