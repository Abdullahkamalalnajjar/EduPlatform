namespace Project.Data.Entities.People
{
    public class Student
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = null!;
        public Project.Data.Entities.Users.ApplicationUser User { get; set; } = null!;

        public int GradeYear { get; set; }
        public string ParentPhoneNumber { get; set; } = null!;

        // link to Parent
        public int? ParentId { get; set; }
        public Parent? Parent { get; set; }

        public ICollection<Project.Data.Entities.Subscriptions.CourseSubscription> CourseSubscriptions { get; set; } = new List<Project.Data.Entities.Subscriptions.CourseSubscription>();
    }
}