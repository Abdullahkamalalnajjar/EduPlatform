using Project.Data.Entities.Content;

namespace Project.EF.Configurations
{
    public class LectureMaterialConfiguration : IEntityTypeConfiguration<LectureMaterial>
    {
        public void Configure(EntityTypeBuilder<LectureMaterial> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FileUrl).IsRequired().HasMaxLength(500);

            builder.HasOne(x => x.Lecture)
                .WithMany(l => l.Materials)
                .HasForeignKey(x => x.LectureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}