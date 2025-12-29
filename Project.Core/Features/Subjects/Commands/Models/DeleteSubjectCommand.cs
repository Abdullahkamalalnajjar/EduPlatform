namespace Project.Core.Features.Subjects.Commands.Models
{
    public class DeleteSubjectCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}