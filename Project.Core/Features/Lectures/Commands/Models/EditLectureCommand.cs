namespace Project.Core.Features.Lectures.Commands.Models
{
    public class EditLectureCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int CourseId { get; set; }
    }
}