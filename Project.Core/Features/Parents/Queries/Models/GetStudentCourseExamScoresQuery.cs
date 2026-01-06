namespace Project.Core.Features.Parents.Queries.Models
{
    public class GetStudentCourseExamScoresQuery : IRequest<Response<IEnumerable<StudentCourseExamDto>>>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
