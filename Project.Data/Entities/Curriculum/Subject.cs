namespace Project.Data.Entities.Curriculum
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // مثال: "رياضة", "عربي"

        public ICollection<Project.Data.Entities.People.Teacher> Teachers { get; set; } = new List<Project.Data.Entities.People.Teacher>();
    }
}