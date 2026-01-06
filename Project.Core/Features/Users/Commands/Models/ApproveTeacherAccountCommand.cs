namespace Project.Core.Features.Users.Commands.Models
{
    public class ApproveTeacherAccountCommand : IRequest<Response<string>>
    {
        public string TeacherUserId { get; set; } = null!;
        
        /// <summary>
        /// Set to true to approve/enable the teacher account, false to disable it
        /// </summary>
        public bool IsApproved { get; set; } = true;
    }
}
