namespace Project.Core.Features.Lectures.Queries.Results
{
    public class LectureMaterialResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public int LectureId { get; set; }
        public bool IsFree { get; set; } = false;

    }
}