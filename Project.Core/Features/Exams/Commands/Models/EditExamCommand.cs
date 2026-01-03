using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Commands.Models
{
    public class EditExamCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int LectureId { get; set; }
        public DateTime? Deadline { get; set; }
        public decimal? DurationInMinutes { get; set; }
    }
}
