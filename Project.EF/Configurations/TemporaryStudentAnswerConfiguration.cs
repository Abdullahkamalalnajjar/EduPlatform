using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Data.Entities.Exams;

namespace Project.EF.Configurations
{
    public class TemporaryStudentAnswerConfiguration : IEntityTypeConfiguration<TemporaryStudentAnswer>
    {
        public void Configure(EntityTypeBuilder<TemporaryStudentAnswer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Exam)
                .WithMany()
                .HasForeignKey(x => x.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.NoAction); // NO ACTION to avoid cascade conflict

            builder.HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.NoAction); // NO ACTION to avoid cascade conflict

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.TextAnswer).IsRequired(false);
            builder.Property(x => x.ImageAnswerUrl).IsRequired(false);
        }
    }
}
