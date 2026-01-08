using Project.Core.Features.Authentication.Commands.Models;

namespace Project.Core.Features.Authentication.Commands.Handlers
{
    public class LogoutCommandHandler : ResponseHandler,
        IRequestHandler<LogoutFromDeviceCommand, Response<string>>,
        IRequestHandler<LogoutFromAllDevicesCommand, Response<string>>
    {
        private readonly ApplicationDbContext _context;

        public LogoutCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(LogoutFromDeviceCommand request, CancellationToken cancellationToken)
        {
            // Get the refresh token (session)
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Id == request.SessionId && rt.ApplicationUserId == request.UserId, cancellationToken);

            if (refreshToken is null)
                return NotFound<string>("Session not found");

            // Revoke the token
            refreshToken.RevokedOn = DateTime.UtcNow;
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Success($"Logged out from device: {refreshToken.DeviceName}");
        }

        public async Task<Response<string>> Handle(LogoutFromAllDevicesCommand request, CancellationToken cancellationToken)
        {
            // Get user with tokens
            var user = await _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user is null)
                return NotFound<string>("User not found");

            // Revoke all active tokens
            var activeTokens = user.RefreshTokens.Where(rt => rt.IsActive).ToList();
            if (!activeTokens.Any())
                return BadRequest<string>("No active sessions to logout from");

            foreach (var token in activeTokens)
            {
                token.RevokedOn = DateTime.UtcNow;
                _context.RefreshTokens.Update(token);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Success($"Logged out from all {activeTokens.Count} device(s)");
        }
    }
}
