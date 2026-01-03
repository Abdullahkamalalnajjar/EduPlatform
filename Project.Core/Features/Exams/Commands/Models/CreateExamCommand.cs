namespace Project.Core.Features.Exams.Commands.Models
{
    public class CreateExamCommand : IRequest<Response<int>>
    {
        public string Title { get; set; } = null!;
        public int LectureId { get; set; }
        public DateTime? Deadline { get; set; }
        public decimal? DurationInMinutes { get; set; }
    }
}
