namespace Project.Data.Dtos
{
    public class AssistantDto
    {
        public int AssistantId { get; set; }
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = null!;
    }
}
