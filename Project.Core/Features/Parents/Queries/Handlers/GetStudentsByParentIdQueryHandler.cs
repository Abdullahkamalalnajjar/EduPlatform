using MediatR;
using Project.Core.Features.Parents.Queries.Models;
using Project.Service.Abstracts;

namespace Project.Core.Features.Parents.Queries.Handlers
{
    public class GetStudentsByParentIdQueryHandler : ResponseHandler, IRequestHandler<GetStudentsByParentIdQuery, Response<IEnumerable<ParentStudentDto>>>
    {
        private readonly IParentService _parentService;

        public GetStudentsByParentIdQueryHandler(IParentService parentService)
        {
            _parentService = parentService;
        }

        public async Task<Response<IEnumerable<ParentStudentDto>>> Handle(GetStudentsByParentIdQuery request, CancellationToken cancellationToken)
        {
            var students = await _parentService.GetStudentsByParentIdAsync(request.ParentId, cancellationToken);

            if (!students.Any())
                return NotFound<IEnumerable<ParentStudentDto>>("No students found for this parent");

            return Success(students);
        }
    }
}
