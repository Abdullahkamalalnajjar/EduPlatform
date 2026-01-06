using MediatR;
using Project.Core.Bases;

namespace Project.Core.Features.Authentication.Command.Models
{
    public class RegisterAssistantCommand : IRequest<Response<string>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        
        // Teacher assigns the assistant
        public int TeacherId { get; set; }
    }
}
