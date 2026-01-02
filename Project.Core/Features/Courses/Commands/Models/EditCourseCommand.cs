namespace Project.Core.Features.Courses.Commands.Models
{
    public class EditCourseCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int TeacherId { get; set; }
        public IFormFile? CourseImageUrl { get; set; }
        public int EducationStageId { get; set; }
    }
}