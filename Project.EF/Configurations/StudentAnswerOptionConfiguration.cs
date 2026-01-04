using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Data.Entities.Exams;

namespace Project.EF.Configurations
{
    public class StudentAnswerOptionConfiguration : IEntityTypeConfiguration<StudentAnswerOption>
    {
        public void Configure(EntityTypeBuilder<StudentAnswerOption> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.StudentAnswer)
                .WithMany(sa => sa.SelectedOptions)
                .HasForeignKey(x => x.StudentAnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.QuestionOption)
                .WithMany()
                .HasForeignKey(x => x.QuestionOptionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Create composite unique index to prevent duplicate selected options
            builder.HasIndex(x => new { x.StudentAnswerId, x.QuestionOptionId })
                .IsUnique()
                .HasDatabaseName("IX_StudentAnswerOption_Unique");
        }
    }
}
