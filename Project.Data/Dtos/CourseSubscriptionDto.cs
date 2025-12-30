namespace Project.Data.Dtos
{
    public class CourseSubscriptionDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
