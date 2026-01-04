using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Data.Entities.Exams;

namespace Project.EF.Configurations
{
    public class StudentAnswerConfiguration : IEntityTypeConfiguration<StudentAnswer>
    {
        public void Configure(EntityTypeBuilder<StudentAnswer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.StudentExamResult)
                .WithMany()
                .HasForeignKey(x => x.StudentExamResultId)
                .OnDelete(DeleteBehavior.NoAction); // NO ACTION to avoid cascade path conflict

            builder.HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SelectedOptions)
                .WithOne(so => so.StudentAnswer)
                .HasForeignKey(so => so.StudentAnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.TextAnswer).IsRequired(false);
            builder.Property(x => x.ImageAnswerUrl).IsRequired(false);
            builder.Property(x => x.PointsEarned).IsRequired(false);
        }
    }
}
