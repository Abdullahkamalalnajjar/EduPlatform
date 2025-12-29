using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Exams.Commands.Models
{
    public class DeleteStudentExamResultCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
