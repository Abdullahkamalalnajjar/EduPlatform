using Project.Data.Entities.Content;

namespace Project.EF.Configurations
{
    public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(300);

            builder.HasOne(x => x.Course)
                .WithMany(c => c.Lectures)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Materials)
                .WithOne(m => m.Lecture)
                .HasForeignKey(m => m.LectureId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Exam)
                .WithOne(e => e.Lecture)
                .HasForeignKey<Project.Data.Entities.Exams.Exam>(e => e.LectureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}