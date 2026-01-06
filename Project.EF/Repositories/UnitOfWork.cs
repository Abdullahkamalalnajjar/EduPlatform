using Project.Data.Entities.People;
using Project.Data.Entities.Curriculum;
using Project.Data.Entities.Subscriptions;
using Project.Data.Entities.Content;
using Project.Data.Entities.Exams;
using Project.Data.Interfaces;

namespace Project.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; private set; }

        public IStudentRepository Students { get; private set; }
        public IParentRepository Parents { get; private set; }
        public ITeacherRepository Teachers { get; private set; }
        public IAssistantRepository Assistants { get; private set; }
        public IAdminRepository Admins { get; private set; }

        public ISubjectRepository Subjects { get; private set; }
        public ICourseRepository Courses { get; private set; }

        public ICourseSubscriptionRepository CourseSubscriptions { get; private set; }

        public ILectureRepository Lectures { get; private set; }
        public ILectureMaterialRepository LectureMaterials { get; private set; }

        public IExamRepository Exams { get; private set; }
        public IQuestionRepository Questions { get; private set; }
        public IQuestionOptionRepository QuestionOptions { get; private set; }
        public IStudentExamResultRepository StudentExamResults { get; private set; }
        public IStudentAnswerRepository StudentAnswers { get; private set; }
        public IStudentAnswerOptionRepository StudentAnswerOptions { get; private set; }
        public ITemporaryStudentAnswerRepository TemporaryStudentAnswers { get; private set; }

        public ITeacherEducationStageRepository TeacherEducationStages { get; private set; }
        public IEducationStageRepository EducationStages { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);

            Students = new StudentRepository(_context);
            Parents = new ParentRepository(_context);
            Teachers = new TeacherRepository(_context);
            Assistants = new AssistantRepository(_context);
            Admins = new AdminRepository(_context);

            Subjects = new SubjectRepository(_context);
            Courses = new CourseRepository(_context);

            CourseSubscriptions = new CourseSubscriptionRepository(_context);

            Lectures = new LectureRepository(_context);
            LectureMaterials = new LectureMaterialRepository(_context);

            Exams = new ExamRepository(_context);
            Questions = new QuestionRepository(_context);
            QuestionOptions = new QuestionOptionRepository(_context);
            StudentExamResults = new StudentExamResultRepository(_context);
            StudentAnswers = new StudentAnswerRepository(_context);
            StudentAnswerOptions = new StudentAnswerOptionRepository(_context);
            TemporaryStudentAnswers = new TemporaryStudentAnswerRepository(_context);

            TeacherEducationStages = new TeacherEducationStageRepository(_context);
            EducationStages = new EducationStageRepository(_context);
        }
        public async Task<int> CompeleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
