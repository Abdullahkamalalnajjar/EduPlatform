namespace Project.Core.Features.Courses.Commands.Models
{
    public class EditCourseCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int GradeYear { get; set; }
        public int TeacherId { get; set; }
    }
}