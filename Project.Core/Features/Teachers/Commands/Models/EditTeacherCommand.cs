using Microsoft.AspNetCore.Http;

namespace Project.Core.Features.Teachers.Commands.Models
{
    public class EditTeacherCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string FacebookUrl { get; set; } = string.Empty;
        public string TelegramUrl { get; set; } = string.Empty;
        public string WhatsAppNumber { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }

        // optional uploaded photo
        public IFormFile? PhotoFile { get; set; }
    }
}
