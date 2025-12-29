using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Queries.Models
{
    public class GetQuestionByIdQuery : IRequest<Response<Project.Core.Features.Exams.Queries.Results.QuestionResponse>>
    {
        public int Id { get; set; }
    }
}
