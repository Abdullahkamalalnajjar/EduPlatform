using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Project.Api.Base;
using Project.Core.Features.Authentication.Command.Models;
using Project.Data.AppMetaData;

namespace Project.Api.Controllers
{

    public class AuthenticationController : AppBaseController
    {
        [HttpPost(Router.AuthenticationRouting.SignUp)]
        public async Task<IActionResult> SignUp([FromForm] SignUpUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(Router.AuthenticationRouting.SginIn)]

        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] CreateNewRefreshTokenCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(Router.AuthenticationRouting.RevokeRefreshToken)]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreashTokenCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(Router.AuthenticationRouting.ResendConfirmEmail)]
        public async Task<IActionResult> ResendConfirmEmail(ResendConfirmEmailCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [Authorize]
        [HttpPost("RegisterAssistant")]
        public async Task<IActionResult> RegisterAssistant([FromBody] RegisterAssistantCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// عرض جميع الجلسات النشطة للمستخدم
        /// Get all active sessions for a user
        /// </summary>
        [Authorize]
        [HttpGet("active-sessions/{userId}")]
        public async Task<IActionResult> GetActiveSessions(
            string userId,
            [FromQuery] string? currentDeviceId)
        {
            var request = new Project.Core.Features.Authentication.Queries.Models.GetActiveSessionsQuery
            {
                UserId = userId,
                CurrentDeviceId = currentDeviceId
            };

            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        /// <summary>
        /// تسجيل الخروج من جهاز معين
        /// Logout from a specific device
        /// </summary>
        [Authorize]
        [HttpPost("logout-device")]
        public async Task<IActionResult> LogoutFromDevice([FromBody] Project.Core.Features.Authentication.Commands.Models.LogoutFromDeviceCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// تسجيل الخروج من جميع الأجهزة
        /// Logout from all devices
        /// </summary>
        [Authorize]
        [HttpPost("logout-all-devices")]
        public async Task<IActionResult> LogoutFromAllDevices([FromBody] Project.Core.Features.Authentication.Commands.Models.LogoutFromAllDevicesCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }

}
