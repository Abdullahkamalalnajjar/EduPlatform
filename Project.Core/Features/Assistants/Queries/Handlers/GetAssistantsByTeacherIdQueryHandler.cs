using MediatR;
using Project.Core.Features.Assistants.Queries.Models;
using Project.Service.Abstracts;

namespace Project.Core.Features.Assistants.Queries.Handlers
{
    public class GetAssistantsByTeacherIdQueryHandler : ResponseHandler, IRequestHandler<GetAssistantsByTeacherIdQuery, Response<IEnumerable<AssistantDto>>>
    {
        private readonly IAssistantService _assistantService;

        public GetAssistantsByTeacherIdQueryHandler(IAssistantService assistantService)
        {
            _assistantService = assistantService;
        }

        public async Task<Response<IEnumerable<AssistantDto>>> Handle(GetAssistantsByTeacherIdQuery request, CancellationToken cancellationToken)
        {
            var assistants = await _assistantService.GetByTeacherIdAsync(request.TeacherId, cancellationToken);

            if (!assistants.Any())
                return NotFound<IEnumerable<AssistantDto>>("No assistants found for this teacher");

            return Success(assistants);
        }
    }
}
