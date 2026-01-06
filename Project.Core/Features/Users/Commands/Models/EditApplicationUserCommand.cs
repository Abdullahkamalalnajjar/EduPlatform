using Microsoft.AspNetCore.Http;

namespace Project.Core.Features.Users.Commands.Models
{
    public class EditApplicationUserCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        // Teacher profile URLs
        public string? PhoneNumber { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TelegramUrl { get; set; }
        public string? YouTubeChannelUrl { get; set; }
        public string? WhatsAppNumber { get; set; }
        
        // Profile photo file
        public IFormFile? PhotoFile { get; set; }
    }
}
