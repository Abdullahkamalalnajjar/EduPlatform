namespace Project.Data.Entities.Content
{
    public class LectureMaterial
    {
        public int Id { get; set; }

        public string Type { get; set; } = null!; // Video, Pdf, Image, Homework
        public string FileUrl { get; set; } = null!;

        public int LectureId { get; set; }
        public Lecture Lecture { get; set; } = null!;
    }
}