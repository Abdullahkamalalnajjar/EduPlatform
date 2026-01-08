using Project.Core.Features.Authentication.Queries.Models;
using Project.Core.Features.Authentication.Queries.Results;

namespace Project.Core.Features.Authentication.Queries.Handlers
{
    public class GetActiveSessionsQueryHandler : ResponseHandler, IRequestHandler<GetActiveSessionsQuery, Response<UserSessionsResponse>>
    {
        private readonly ApplicationDbContext _context;

        public GetActiveSessionsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<UserSessionsResponse>> Handle(GetActiveSessionsQuery request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user is null)
                return NotFound<UserSessionsResponse>("User not found");

            // Get active refresh tokens for this user
            var now = DateTime.UtcNow;
            var activeSessions = await _context.RefreshTokens
                .Where(rt => rt.ApplicationUserId == request.UserId && rt.RevokedOn == null && rt.ExpiresOn > now)
                .Select(rt => new ActiveSessionResponse
                {
                    SessionId = rt.Id,
                    DeviceId = rt.DeviceId ?? "Unknown",
                    DeviceName = rt.DeviceName ?? "Unknown Device",
                    IpAddress = rt.IpAddress ?? "Unknown",
                    CreatedAt = rt.CreatedOn,
                    LastActivityAt = rt.LastActivityAt,
                    ExpiresAt = rt.ExpiresOn,
                    IsCurrentSession = rt.DeviceId == request.CurrentDeviceId
                })
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync(cancellationToken);

            var response = new UserSessionsResponse
            {
                UserId = user.Id,
                Email = user.Email,
                ActiveSessions = activeSessions,
                TotalActiveSessions = activeSessions.Count
            };

            return Success(response);
        }
    }
}
