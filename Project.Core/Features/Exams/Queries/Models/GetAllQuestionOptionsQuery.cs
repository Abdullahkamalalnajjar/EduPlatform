using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Queries.Models
{
    public class GetAllQuestionOptionsQuery : IRequest<Response<IEnumerable<Project.Core.Features.Exams.Queries.Results.QuestionOptionResponse>>> { }
}
