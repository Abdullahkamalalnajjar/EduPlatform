using Project.Api.Base;
using Project.Core.Features.Claims.Queries.Models;
using Project.Data.Consts;
using Project.Data.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Project.Api.Controllers
{

    public class ClaimController : AppBaseController
    {
        [HasPermission(Permissions.GetClaims)]
        [HttpGet("GetClaims")]
        public async Task<IActionResult> GetClaims()
        {
            var query = new GetPermissionsQuery();
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HasPermission(Permissions.GetClaims)]
        [HttpGet("GetClaims/{id}")]
        public async Task<IActionResult> GetClaims(string id)
        {
            var query = new GetPermissionsByRoleIdQuery { RoleId = id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }

    }
}
