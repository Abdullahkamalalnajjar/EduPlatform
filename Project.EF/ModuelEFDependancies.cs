namespace Project.EF
{
    public static class ModuelEFDependancies
    {
        public static IServiceCollection AddEFDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();

            // Repositories for entities
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IParentRepository, ParentRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IAssistantRepository, AssistantRepository>();

            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();

            services.AddTransient<ICourseSubscriptionRepository, CourseSubscriptionRepository>();

            services.AddTransient<ILectureRepository, LectureRepository>();
            services.AddTransient<ILectureMaterialRepository, LectureMaterialRepository>();

            services.AddTransient<IExamRepository, ExamRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuestionOptionRepository, QuestionOptionRepository>();
            services.AddTransient<IStudentExamResultRepository, StudentExamResultRepository>();
            services.AddTransient<IEducationStageRepository, EducationStageRepository>();
            services.AddTransient<IStudentAnswerRepository, StudentAnswerRepository>();
            services.AddTransient<IStudentAnswerOptionRepository, StudentAnswerOptionRepository>();
            services.AddTransient<ITemporaryStudentAnswerRepository, TemporaryStudentAnswerRepository>();
            return services;
        }
    }
}
