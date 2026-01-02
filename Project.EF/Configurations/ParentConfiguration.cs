using Project.Data.Entities.People;

namespace Project.EF.Configurations
{
    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ApplicationUserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithOne(u => u.ParentProfile)
                .HasForeignKey<Parent>(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.ParentPhoneNumber).HasMaxLength(100);

            builder.HasMany(x => x.Children)
                .WithOne(s => s.Parent)
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}