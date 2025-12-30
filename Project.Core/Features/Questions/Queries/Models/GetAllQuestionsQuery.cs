using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Questions.Queries.Models
{
    public class GetAllQuestionsQuery : IRequest<Response<IEnumerable<Project.Core.Features.Questions.Queries.Results.QuestionResponse>>> { }
}
