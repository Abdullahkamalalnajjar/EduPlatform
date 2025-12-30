using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Teachers.Commands.Models
{
    public class AddTeacherEducationStageCommand : IRequest<Response<int>>
    {
        public int TeacherId { get; set; }
        public int EducationStageId { get; set; }
    }
}
