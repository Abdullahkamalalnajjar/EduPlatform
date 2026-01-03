using Project.Data.Entities.Exams;

namespace Project.EF.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Lecture)
                .WithOne(l => l.Exam)
                .HasForeignKey<Exam>(x => x.LectureId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.DurationInMinutes).HasColumnType("decimal(8,2)");

            builder.HasMany(x => x.Questions)
                .WithOne(q => q.Exam)
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}