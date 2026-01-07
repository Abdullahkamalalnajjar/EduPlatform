namespace Project.Core.Features.Subjects.Commands.Models
{
    public class CreateSubjectCommand : IRequest<Response<int>>
    {
        public string Name { get; set; } = null!;
        public IFormFile? SubjectImageFile { get; set; }
    }
}