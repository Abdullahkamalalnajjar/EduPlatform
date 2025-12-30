using Microsoft.Extensions.DependencyInjection;

namespace Project.Service
{
    public static class ModuelServiceDependancies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddTransient<IEmailSender, EmailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IClaimService, ClaimService>();

            // New entity services
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IParentService, ParentService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IAssistantService, AssistantService>();

            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<ICourseService, CourseService>();

            services.AddTransient<ICourseSubscriptionService, CourseSubscriptionService>();

            services.AddTransient<ILectureService, LectureService>();
            services.AddTransient<ILectureMaterialService, LectureMaterialService>();

            services.AddTransient<IExamService, ExamService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IQuestionOptionService, QuestionOptionService>();
            services.AddTransient<IStudentExamResultService, StudentExamResultService>();


            return services;
        }
    }
}
