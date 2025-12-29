using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Data.Entities.People;

namespace Project.EF.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ApplicationUserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithOne(u => u.StudentProfile)
                .HasForeignKey<Student>(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ParentPhoneNumber).HasMaxLength(50);

            builder.HasMany(x => x.CourseSubscriptions)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}