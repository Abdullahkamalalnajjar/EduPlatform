using Project.Data.Entities.Exams;

namespace Project.EF.Configurations
{
    public class StudentExamResultConfiguration : IEntityTypeConfiguration<StudentExamResult>
    {
        public void Configure(EntityTypeBuilder<StudentExamResult> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Student)
                .WithMany() // not ideal, but StudentExamResult doesn't have collection configured; keep simple relation
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Exam)
                .WithMany() // not ideal; Exam->Questions exists; adjust if you want separate navigation
                .HasForeignKey(x => x.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.TotalScore).IsRequired();
        }
    }
}