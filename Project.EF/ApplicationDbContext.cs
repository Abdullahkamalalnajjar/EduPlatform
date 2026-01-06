using Project.Data.Entities.Content;
using Project.Data.Entities.Curriculum;
using Project.Data.Entities.Exams;
using Project.Data.Entities.People;
using Project.Data.Entities.Subscriptions;
using Project.Data.Entities.Users;

namespace Project.EF
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add your custom model configurations here  
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        // People
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Assistant> Assistants { get; set; }
        public DbSet<Admin> Admins { get; set; }

        // Curriculum
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<EducationStage> EducationStages { get; set; }

        // Subscriptions
        public DbSet<CourseSubscription> CourseSubscriptions { get; set; }

        // Content
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<LectureMaterial> LectureMaterials { get; set; }

        // Exams
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Project.Data.Entities.Exams.StudentExamResult> StudentExamResults { get; set; }
        public DbSet<Project.Data.Entities.Exams.StudentAnswer> StudentAnswers { get; set; }
        public DbSet<Project.Data.Entities.Exams.StudentAnswerOption> StudentAnswerOptions { get; set; }
        public DbSet<Project.Data.Entities.Exams.TemporaryStudentAnswer> TemporaryStudentAnswers { get; set; }

        // Teacher Education stages
        public DbSet<TeacherEducationStage> TeacherEducationStages { get; set; }
    }
}
