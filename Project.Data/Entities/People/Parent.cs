using Project.Data.Entities.Users;

namespace Project.Data.Entities.People
{
    public class Parent
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public string NationalId { get; set; } = null!;

        public ICollection<Student> Children { get; set; } = new List<Student>();
    }
}