using Project.Api.Base;
using Project.Core.Features.Users.Commands.Models;
using Project.Core.Features.Users.Queries.Models;
using Project.Data.Consts;
using Project.Data.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Project.Api.Controllers
{

    public class UserController : AppBaseController
    {
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HasPermission(Permissions.GetUsers)]
        [HttpGet("AllUser")]
        public async Task<IActionResult> AllUsers()
        {
            var request = new GetAllUserQuery();
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var request = new GetUserByIdQuery { UserId = userId };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
