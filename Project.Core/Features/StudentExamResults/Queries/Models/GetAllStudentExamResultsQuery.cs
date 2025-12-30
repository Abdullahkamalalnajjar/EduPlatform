using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.StudentExamResults.Queries.Models
{
    public class GetAllStudentExamResultsQuery : IRequest<Response<IEnumerable<Project.Core.Features.StudentExamResults.Queries.Results.StudentExamResultResponse>>> { }
}
