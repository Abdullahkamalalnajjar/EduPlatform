using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Data.Entities.Exams;

namespace Project.EF.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.QuestionType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.AnswerType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Content).IsRequired();

            builder.HasMany(x => x.Options)
                .WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}