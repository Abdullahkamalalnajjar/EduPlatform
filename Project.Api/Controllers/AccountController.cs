using Project.Api.Base;
using Project.Core.Features.Users.Commands.Models;
using Project.Core.Features.Users.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Api.Controllers
{
    [Authorize]
    public class AccountController : AppBaseController
    {
        [HttpGet("UserProfile")]
        public async Task<IActionResult> Info(string userId)
        {
            var request = new UserProfileQuery { UserId = userId };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPut("EditUserProfile")]
        public async Task<IActionResult> UpdateInfo(EditApplicationUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    
    }
}
