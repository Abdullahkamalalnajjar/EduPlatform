using Project.Data.Entities.Content;
using Project.Data.Entities.Curriculum;
using Project.Data.Entities.Exams;
using Project.Data.Entities.People;
using Project.Data.Entities.Subscriptions;
using Project.Data.Entities.Users;

namespace Project.EF
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
        : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

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

        // Curriculum
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Subscriptions
        public DbSet<CourseSubscription> CourseSubscriptions { get; set; }

        // Content
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<LectureMaterial> LectureMaterials { get; set; }

        // Exams
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Data.Entities.Exams.StudentExamResult> StudentExamResults { get; set; }
    }
}
