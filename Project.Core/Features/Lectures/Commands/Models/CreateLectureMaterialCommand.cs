namespace Project.Core.Features.Lectures.Commands.Models
{
    public class CreateLectureMaterialCommand : IRequest<Response<int>>
    {
        public string Type { get; set; } = null!; // Video, Pdf, Image
        public string FileUrl { get; set; } = null!;
        public int LectureId { get; set; }
    }
}