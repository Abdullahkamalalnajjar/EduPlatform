using Project.Data.Entities.People;

namespace Project.EF.Configurations
{
    public class AssistantConfiguration : IEntityTypeConfiguration<Assistant>
    {
        public void Configure(EntityTypeBuilder<Assistant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ApplicationUserId).IsRequired();

            builder.HasOne(x => x.User)
                .WithOne(u => u.AssistantProfile)
                .HasForeignKey<Assistant>(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Teacher)
                .WithMany() // assistant linked to teacher; if you want separate relation adjust
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}