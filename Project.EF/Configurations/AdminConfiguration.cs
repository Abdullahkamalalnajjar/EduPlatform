using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Data.Entities.People;

namespace Project.EF.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.ApplicationUserId).IsUnique();

            builder.Property(x => x.ApplicationUserId).IsRequired();

            builder.HasOne(x => x.User)
                .WithOne(u => u.AdminProfile)
                .HasForeignKey<Admin>(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
