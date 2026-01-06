namespace Project.Data.Dtos
{
    public class GetAllUsersResponse
    {
        public ICollection<UserStudentDto> Students { get; set; } = new List<UserStudentDto>();
        public ICollection<UserTeacherDto> Teachers { get; set; } = new List<UserTeacherDto>();
        public ICollection<UserParentDto> Parents { get; set; } = new List<UserParentDto>();
    }

    public class UserStudentDto
    {
        public int StudentId { get; set; }
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int GradeYear { get; set; }
        public string ParentPhoneNumber { get; set; } = null!;
    }

    public class UserTeacherDto
    {
        public int TeacherId { get; set; }
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? PhotoUrl { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public bool? IsVerified { get; set; }
    }

    public class UserParentDto
    {
        public int ParentId { get; set; }
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string ParentPhoneNumber { get; set; } = null!;
        public int ChildrenCount { get; set; }
    }
}
