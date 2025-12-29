namespace Project.Core.Features.Lectures.Commands.Models
{
    public class CreateLectureCommand : IRequest<Response<int>>
    {
        public string Title { get; set; } = null!;
        public int CourseId { get; set; }
    }
}