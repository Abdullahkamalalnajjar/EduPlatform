using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.Roles.Commands.Models;
using Project.Core.Features.Roles.Queries.Queries.Models;
using Project.Data.AppMetaData;
using Project.Data.Consts;
using Project.Data.Filters;

namespace Project.Api.Controllers
{

    public class RoleController : AppBaseController
    {
        [HasPermission(Permissions.GetRoles)]
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles(bool? includeDeleted)
        {
            var request = new GetRolesQuery { IncludeDeleted = includeDeleted };
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HasPermission(Permissions.GetRoles)]
        [HttpGet("GetRolesDetails")]
        public async Task<IActionResult> GetRolesDetails(string roleId)
        {
            var request = new GetRolesDetailsQuery { RoleId = roleId };
            var response = await Mediator.Send(request);
            return Ok(response);
        }
        [HasPermission(Permissions.AddRoles)]
        [HttpPost(Router.RoleRouting.Create)]
        public async Task<IActionResult> AddRoleWithPermissions(CreateRoleWithPermissionsCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
        [HasPermission(Permissions.UpdateRoles)]
        [HttpPut(Router.RoleRouting.Edit)]
        public async Task<IActionResult> EditRoleWithPermissions(EditRoleWithPermissionsCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }

}
