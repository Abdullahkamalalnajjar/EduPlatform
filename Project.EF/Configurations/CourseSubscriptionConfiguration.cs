using Project.Data.Entities.Subscriptions;

namespace Project.EF.Configurations
{
    public class CourseSubscriptionConfiguration : IEntityTypeConfiguration<CourseSubscription>
    {
        public void Configure(EntityTypeBuilder<CourseSubscription> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);

            builder.HasOne(x => x.Student)
                .WithMany(s => s.CourseSubscriptions)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Course)
                .WithMany() // Note: this is not correct if course->lectures; adjust if needed
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}