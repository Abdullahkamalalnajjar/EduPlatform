using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Queries.Models
{
    public class GetQuestionOptionByIdQuery : IRequest<Response<Project.Core.Features.Exams.Queries.Results.QuestionOptionResponse>>
    {
        public int Id { get; set; }
    }
}
