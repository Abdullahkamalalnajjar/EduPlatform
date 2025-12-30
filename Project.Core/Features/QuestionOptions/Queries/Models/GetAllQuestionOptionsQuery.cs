using Project.Core.Features.QuestionOptions.Queries.Results;

namespace Project.Core.Features.QuestionOptions.Queries.Models
{
    public class GetAllQuestionOptionsQuery : IRequest<Response<IEnumerable<QuestionOptionResponse>>>
    {
    }
}
