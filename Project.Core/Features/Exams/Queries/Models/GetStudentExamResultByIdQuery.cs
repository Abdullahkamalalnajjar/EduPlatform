using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Queries.Models
{
    public class GetStudentExamResultByIdQuery : IRequest<Response<Project.Core.Features.Exams.Queries.Results.StudentExamResultResponse>>
    {
        public int Id { get; set; }
    }
}
