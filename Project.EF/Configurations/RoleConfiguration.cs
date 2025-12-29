using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Data.Entities.Users;
using Project.Data.Consts;

namespace Project.EF.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            // Default Data
            builder.HasData(
                new ApplicationRole
                {
                    Id = DefaultRoles.AdminRoleId,
                    Name = DefaultRoles.Admin,
                    NormalizedName = DefaultRoles.Admin.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp,
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.TeacherRoleId,
                    Name = DefaultRoles.Teacher,
                    NormalizedName = DefaultRoles.Teacher.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.TeacherRoleConcurrencyStamp,
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.ParentRoleId,
                    Name = DefaultRoles.Parent,
                    NormalizedName = DefaultRoles.Parent.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.ParentRoleConcurrencyStamp,
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.StudentRoleId,
                    Name = DefaultRoles.Student,
                    NormalizedName = DefaultRoles.Student.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.StudentRoleConcurrencyStamp,
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.AssistantRoleId,
                    Name = DefaultRoles.Assistant,
                    NormalizedName = DefaultRoles.Assistant.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.AssistantRoleConcurrencyStamp,
                }
            );
        }
    }
}
