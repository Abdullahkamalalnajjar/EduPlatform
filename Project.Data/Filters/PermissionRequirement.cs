using Microsoft.AspNetCore.Authorization;

namespace Project.Data.Filters;
public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}