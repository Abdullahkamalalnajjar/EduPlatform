namespace Project.Core.Features.Authentication.Command.Models
{
    public class SignInCommand : IRequest<Response<AuthResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? DeviceId { get; set; } // معرّف فريد للجهاز
        public string? DeviceName { get; set; } // اسم الجهاز
    }
}
