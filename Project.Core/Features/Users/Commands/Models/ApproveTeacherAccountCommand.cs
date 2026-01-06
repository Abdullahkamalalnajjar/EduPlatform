namespace Project.Core.Features.Users.Commands.Models
{
    public class ApproveTeacherAccountCommand : IRequest<Response<string>>
    {
        public string TeacherUserId { get; set; } = null!;
        public string ApprovalNotes { get; set; } = string.Empty;
    }
}
