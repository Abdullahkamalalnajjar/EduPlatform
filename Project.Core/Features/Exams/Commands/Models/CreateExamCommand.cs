using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Commands.Models
{
    public class CreateExamCommand : IRequest<Response<int>>
    {
        public int LectureId { get; set; }
    }
}
