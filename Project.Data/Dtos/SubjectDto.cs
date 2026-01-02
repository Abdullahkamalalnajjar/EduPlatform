namespace Project.Data.Dtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // مثال: "رياضة", "عربي"
        public string? SubjectImageUrl { get; set; } = null!;

        public ICollection<TeacherDto> Teachers { get; set; } = new List<TeacherDto>();
    }

    public class TeacherDto
    {
        public int Id { get; set; }
        public string TeacherName { get; set; } = null!;
        public ICollection<TeacherEducationStageDto> TeacherEducationStages { get; set; } = new List<TeacherEducationStageDto>();
        public string SubjectName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FacebookUrl { get; set; } = null!;
        public string TelegramUrl { get; set; } = null!;
        public string WhatsAppNumber { get; set; } = null!;
        public string? PhotoUrl { get; set; }
        public ICollection<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
    public class TeacherEducationStageDto
    {
        public int Id { get; set; }
        public string EducationStageName { get; set; } = null!;
    }

}

