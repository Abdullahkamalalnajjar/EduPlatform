namespace Project.Core.Features.Subjects.Queries.Results
{
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? SubjectImageUrl { get; set; } = null!;

    }
}