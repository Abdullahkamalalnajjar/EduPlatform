namespace Project.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IUserRepository Users { get; }
        IStudentRepository Students { get; }
        IParentRepository Parents { get; }
        ITeacherRepository Teachers { get; }
        IAssistantRepository Assistants { get; }

        ISubjectRepository Subjects { get; }
        ICourseRepository Courses { get; }

        ICourseSubscriptionRepository CourseSubscriptions { get; }

        ILectureRepository Lectures { get; }
        ILectureMaterialRepository LectureMaterials { get; }

        IExamRepository Exams { get; }
        IQuestionRepository Questions { get; }
        IQuestionOptionRepository QuestionOptions { get; }
        IStudentExamResultRepository StudentExamResults { get; }

        Task<int> CompeleteAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}
