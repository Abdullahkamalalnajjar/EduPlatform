using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Commands.Models
{
    public class EditExamCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
    }
}
