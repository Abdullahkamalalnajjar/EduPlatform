using Microsoft.AspNetCore.Authorization;

namespace Project.Data.Filters;
public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}