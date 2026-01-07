namespace Project.Core.Features.Courses.Commands.Models
{
    public class CreateCourseCommand : IRequest<Response<int>>
    {
        public string Title { get; set; } = null!;
        public int TeacherId { get; set; }
        public IFormFile? CourseImageUrl { get; set; }

        public int EducationStageId { get; set; }

        public decimal? Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
    }
}