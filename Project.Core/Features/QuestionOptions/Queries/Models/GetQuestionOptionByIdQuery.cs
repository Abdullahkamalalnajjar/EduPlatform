namespace Project.Core.Features.QuestionOptions.Queries.Models
{
    public class GetQuestionOptionByIdQuery : IRequest<Response<Project.Core.Features.QuestionOptions.Queries.Results.QuestionOptionResponse>>
    {
        public int Id { get; set; }
    }
}
