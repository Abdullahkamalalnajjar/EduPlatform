namespace Project.Core.Features.Authentication.Commands.Models
{
    public class LogoutFromDeviceCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; } = null!;
        public int SessionId { get; set; } // ????? ?????? (RefreshToken ID)
    }

    public class LogoutFromAllDevicesCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; } = null!;
    }
}
