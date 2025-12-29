namespace Project.Core.Features.Authentication.Command.Models
{
    public class SignUpUserCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

}
