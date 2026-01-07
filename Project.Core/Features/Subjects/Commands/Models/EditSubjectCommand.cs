namespace Project.Core.Features.Subjects.Commands.Models
{
    public class EditSubjectCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IFormFile? SubjectImageFile { get; set; }
    }
}