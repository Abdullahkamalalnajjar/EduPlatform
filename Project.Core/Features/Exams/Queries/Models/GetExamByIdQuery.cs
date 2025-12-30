namespace Project.Core.Features.Exams.Queries.Models
{
    public class GetExamByIdQuery : IRequest<Response<Project.Core.Features.Exams.Queries.Results.ExamResponse>>
    {
        public int Id { get; set; }
    }
}
