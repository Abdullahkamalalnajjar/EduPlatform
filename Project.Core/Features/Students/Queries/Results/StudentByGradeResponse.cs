namespace Project.Core.Features.Students.Queries.Results
{
    public class StudentByGradeResponse
    {
        public int Id { get; set; }
        public int GradeYear { get; set; }
        public string UserId { get; set; } = null!;
        public string FullName { get; set; } = null!;
    }
}
