using Project.Data.Entities.Users;

namespace Project.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            //Default Data
            var admin = new ApplicationUser
            {
                Id = DefaultUsers.AdminId,
                FirstName = "Edu",
                LastName = "Platform",
                UserName = DefaultUsers.AdminEmail,
                NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
                Email = DefaultUsers.AdminEmail,
                NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
                SecurityStamp = DefaultUsers.AdminSecurityStamp,
                ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
                EmailConfirmed = true,
            };

            // استخدم PasswordHasher لتوليد الـ PasswordHash
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = "AQAAAAIAAYagAAAAEBDQtLhx3P3q2s2VUfY4MQ4YW8CK+Utz+LJ36vMVUX00IxkwbNR5aVSWIjAIRU+Dgg==";

            builder.HasData(admin);


        }
    }
}
