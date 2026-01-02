namespace Project.Data.Dtos
{
    public class CourseSubscriptionDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Status { get; set; } // Pending - Approved - Rejected
        public DateTime CreatedAt { get; set; }
        public ICollection<LectureDto> Lectures { get; set; } = new List<LectureDto>();

    }
}
