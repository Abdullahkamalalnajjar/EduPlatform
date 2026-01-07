namespace Project.Data.Dtos
{
    public class AdminStatisticsResponse
    {
        /// <summary>
        /// Total number of teachers (????????)
        /// </summary>
        public int TotalTeachers { get; set; }

        /// <summary>
        /// Total registered users (?????? ????????)
        /// </summary>
        public int TotalRegisteredUsers { get; set; }

        /// <summary>
        /// Total number of guardians/parents (?????? ??????)
        /// </summary>
        public int TotalParents { get; set; }

        /// <summary>
        /// Total number of students (??????)
        /// </summary>
        public int TotalStudents { get; set; }

        /// <summary>
        /// Total number of exams (??????????)
        /// </summary>
        public int TotalExams { get; set; }

        /// <summary>
        /// Total number of courses (???????)
        /// </summary>
        public int TotalCourses { get; set; }

        /// <summary>
        /// Percentage change for teachers (e.g., "+8 ??? ?????")
        /// </summary>
        public string TeachersChangeMessage { get; set; } = "";

        /// <summary>
        /// Percentage change for registered users (e.g., "+12 ??? ?????")
        /// </summary>
        public string RegisteredUsersChangeMessage { get; set; } = "";

        /// <summary>
        /// Percentage change for parents (e.g., "+8 ???? ????")
        /// </summary>
        public string ParentsChangeMessage { get; set; } = "";

        /// <summary>
        /// Percentage change for students (e.g., "+23 ????")
        /// </summary>
        public string StudentsChangeMessage { get; set; } = "";

        /// <summary>
        /// Percentage change for exams (e.g., "+42 ??????")
        /// </summary>
        public string ExamsChangeMessage { get; set; } = "";

        /// <summary>
        /// Percentage change for courses (e.g., "+15 ????")
        /// </summary>
        public string CoursesChangeMessage { get; set; } = "";
    }
}
