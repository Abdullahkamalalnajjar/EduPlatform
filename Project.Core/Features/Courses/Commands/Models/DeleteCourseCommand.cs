namespace Project.Core.Features.Courses.Commands.Models
{
    public class DeleteCourseCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}