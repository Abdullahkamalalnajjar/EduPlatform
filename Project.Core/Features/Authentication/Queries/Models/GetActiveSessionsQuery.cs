using Project.Core.Features.Authentication.Queries.Results;

namespace Project.Core.Features.Authentication.Queries.Models
{
    public class GetActiveSessionsQuery : IRequest<Response<UserSessionsResponse>>
    {
        public string UserId { get; set; } = null!;
        public string? CurrentDeviceId { get; set; } // ????? ?????? ?????? ???????
    }
}
