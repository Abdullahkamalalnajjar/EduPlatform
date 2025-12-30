using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Data.Entities.People;

namespace Project.EF.Configurations
{
    public class TeacherEducationStageConfiguration : IEntityTypeConfiguration<TeacherEducationStage>
    {
        public void Configure(EntityTypeBuilder<TeacherEducationStage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(ts => ts.Teacher)
                .WithMany(t => t.TeacherEducationStages)
                .HasForeignKey(ts => ts.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ts => ts.EducationStage)
                .WithMany(es => es.TeacherEducationStages)
                .HasForeignKey(ts => ts.EducationStageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ts => new { ts.TeacherId, ts.EducationStageId }).IsUnique();
        }
    }
}
