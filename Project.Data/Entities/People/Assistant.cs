namespace Project.Data.Entities.People
{
    public class Assistant
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = null!;
        public Project.Data.Entities.Users.ApplicationUser User { get; set; } = null!;

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
    }
}