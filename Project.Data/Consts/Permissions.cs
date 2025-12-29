namespace Project.Data.Consts
{
    public static class Permissions
    {
        public static string Type { get; } = "permissions"; //properity




        public const string GetRoles = "roles:read";
        public const string AddRoles = "roles:add";
        public const string UpdateRoles = "roles:update";

        public const string GetClaims = "claims:read";
        public const string AddClaims = "claims:add";
        public const string UpdateClaims = "claims:update";
        public const string DeleteClaims = "claims:delete";

        public const string GetUsers = "users:read";
        public const string AddUsers = "users:add";
        public const string UpdateUsers = "users:update";
        public const string DeleteUsers = "users:delete";
        public static IList<string?> GetAllPermissions() => typeof(Permissions).GetFields().Select(x => x.GetValue(x) as string).ToList();
    }
}
