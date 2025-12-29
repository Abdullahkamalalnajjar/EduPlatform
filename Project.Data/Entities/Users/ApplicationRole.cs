using Microsoft.AspNetCore.Identity;

namespace Project.Data.Entities.Users
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsDefualt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
