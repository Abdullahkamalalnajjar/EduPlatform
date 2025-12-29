namespace Project.Data.Entities.Subscriptions
{
    public class CourseSubscription
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Project.Data.Entities.People.Student Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Project.Data.Entities.Curriculum.Course Course { get; set; } = null!;

        public string Status { get; set; } = "Pending"; // Pending - Approved - Rejected

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}