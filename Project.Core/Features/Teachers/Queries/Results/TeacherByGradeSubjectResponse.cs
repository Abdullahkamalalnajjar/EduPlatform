namespace Project.Core.Features.Teachers.Queries.Results
{
    public class TeacherByGradeSubjectResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string ApplicationUserId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public string WhatAppNumber { get; set; }
        public string TeacherProfile { get; set; } = null!;
        public List<CourseDtoo> Courses { get; set; } = new();
    }

    public class CourseDtoo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string EducationStageName { get; set; } = null!;
        public List<LectureDtoo> Lectures { get; set; } = new();
    }

    public class LectureDtoo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<MaterialDtoo> Materials { get; set; } = new();
    }

    public class MaterialDtoo
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public bool IsFree { get; set; }
    }
}
