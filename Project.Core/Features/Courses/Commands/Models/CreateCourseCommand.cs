namespace Project.Core.Features.Courses.Commands.Models
{
    public class CreateCourseCommand : IRequest<Response<int>>
    {
        public string Title { get; set; } = null!;
        public int GradeYear { get; set; }
        public int TeacherId { get; set; }
    }
}