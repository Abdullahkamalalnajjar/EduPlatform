namespace Project.Core.Features.Lectures.Commands.Models
{
    public class EditLectureMaterialCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public int LectureId { get; set; }
    }
}