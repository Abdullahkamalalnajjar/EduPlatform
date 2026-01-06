using Project.Data.Entities.Users;

namespace Project.Data.Entities.People
{
    public class Admin
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
