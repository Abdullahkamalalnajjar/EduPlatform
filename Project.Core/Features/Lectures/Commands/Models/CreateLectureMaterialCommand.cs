namespace Project.Core.Features.Lectures.Commands.Models
{
    public class CreateLectureMaterialCommand : IRequest<Response<int>>
    {
        public string Type { get; set; } = null!; // Video, Pdf, Image
        public string VideoUrl { get; set; } = null!;
        public int LectureId { get; set; }
        public bool IsFree { get; set; } = false;

        // optional uploaded file (image or pdf)
        public IFormFile? File { get; set; }
    }
}