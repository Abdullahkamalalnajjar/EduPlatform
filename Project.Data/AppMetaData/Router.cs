namespace Project.Data.AppMetaData
{
    public static class Router
    {
        private const string root = "api"; // use lowercase
        private const string version = "v1"; // use lowercase
        private const string Rule = root + "/" + version;

        // Common single id pattern
        private const string IdSegment = "{id}";

        public static class GoogleRouting
        {
            private const string Prefix = Rule + "/google";
            public const string GoogleSignIn = Prefix + "/signin";
            public const string GoogleLoginCallback = Prefix + "/callback";
        }

        public static class PaymentRouting
        {
            private const string Prefix = Rule + "/payments";
            public const string CreatePayment = Prefix;
            public const string GetPaymentById = Prefix + "/" + IdSegment;
            public const string GetPaymentsForUser = Prefix + "/user/{userId}";
            public const string GetPaymentIntent = Prefix + "/intent/{basketId}";
            public const string UpdatePaymentStatus = Prefix + "/status/{paymentId}";
        }

        public static class EmailRouting
        {
            private const string Prefix = Rule + "/email";
            public const string SendEmail = Prefix + "/send";
        }

        public static class UserRouting
        {
            private const string Prefix = Rule + "/users";
            public const string List = Prefix; // GET /api/v1/users
            public const string GetById = Prefix + "/" + IdSegment; // GET /api/v1/users/{id}
            public const string Create = Prefix; // POST /api/v1/users
            public const string Edit = Prefix + "/" + IdSegment; // PUT /api/v1/users/{id}
            public const string Delete = Prefix + "/" + IdSegment; // DELETE /api/v1/users/{id}
            public const string Paginated = Prefix + "/paginated";
            public const string ChangePassword = Prefix + "/change-password";
        }

        public static class RoleRouting
        {
            private const string Prefix = Rule + "/roles";
            public const string List = Prefix;
            public const string GetById = Prefix + "/" + IdSegment;
            public const string Create = Prefix;
            public const string Edit = Prefix + "/" + IdSegment;
            public const string Delete = Prefix + "/" + IdSegment;
            public const string Paginated = Prefix + "/paginated";
        }

        public static class SubjectRouting
        {
            private const string Prefix = Rule + "/subjects";
            public const string List = Prefix;
            public const string GetById = Prefix + "/" + IdSegment;
            public const string Create = Prefix;
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/" + IdSegment;
            public const string Paginated = Prefix + "/paginated";
        }

        public static class StudentRouting
        {
            private const string Prefix = Rule + "/students";
            public const string List = Prefix;
            public const string GetById = Prefix + "/" + IdSegment;
            public const string Create = Prefix;
            public const string Edit = Prefix + "/" + IdSegment;
            public const string Delete = Prefix + "/" + IdSegment;
            public const string Paginated = Prefix + "/paginated";
        }

        public static class TeacherRouting
        {
            private const string Prefix = Rule + "/teachers";
            public const string List = Prefix;
            public const string GetById = Prefix + "/" + IdSegment;
            public const string Create = Prefix;
            public const string Edit = Prefix + "/" + IdSegment;
            public const string Delete = Prefix + "/" + IdSegment;
            public const string Paginated = Prefix + "/paginated";
        }

        public static class CourseRouting
        {
            private const string Prefix = Rule + "/courses";
            public const string List = Prefix;
            public const string GetById = Prefix + "/" + IdSegment;
            public const string Create = Prefix;
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/" + IdSegment;
            public const string Paginated = Prefix + "/paginated";
            public const string GetCoursesBySubject = Prefix + "/subject/" + IdSegment;
        }

        public static class LectureRouting
        {
            private const string Prefix = Rule + "/lectures";
            public const string List = Prefix;
            public const string GetById = Prefix + "/" + IdSegment;
            public const string Create = Prefix;
            // public const string Edit = Prefix + "/" + IdSegment;
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/" + IdSegment;
            public const string Paginated = Prefix + "/paginated";
        }

        public static class ExamRouting
        {
            private const string Prefix = Rule + "/exams";
            public const string List = Prefix;
            public const string GetById = Prefix + "/" + IdSegment;
            public const string Create = Prefix;
            // public const string Edit = Prefix + "/" + IdSegment;
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/" + IdSegment;
            public const string Paginated = Prefix + "/paginated";
        }

        public static class CourseSubscriptionRouting
        {
            private const string Prefix = Rule + "/course-subscriptions";
            public const string List = Prefix;
            public const string GetById = Prefix + "/" + IdSegment;
            public const string Create = Prefix;
            public const string Edit = Prefix + "/" + IdSegment;
            public const string Delete = Prefix + "/" + IdSegment;
            public const string Paginated = Prefix + "/paginated";
            public const string GetCoursesByStudent = Prefix + "/student/" + "studentId";

        }

        public static class AuthenticationRouting
        {
            private const string Prefix = Rule + "/auth";
            public const string SginIn = Prefix + "/signin";
            public const string SignUp = Prefix + "/signup";
            public const string RefreshToken = Prefix + "/refresh-token";
            public const string RevokeRefreshToken = Prefix + "/revoke-refresh-token";
            public const string ChangePassword = Prefix + "/change-password";
            public const string SendOtp = Prefix + "/send-otp";
            public const string Verifyotp = Prefix + "/verify-otp";
            public const string ResetPassword = Prefix + "/reset-password";
            public const string ConfirmEmail = Prefix + "/confirm-email";
            public const string ResendConfirmEmail = Prefix + "/resend-confirm-email";
        }

        public static class AuthorizationRouting
        {
            private const string Prefix = Rule + "/authorization";
            public const string CreateRole = Prefix + "/create-role";
            public const string MangeUserRoles = Prefix + "/manage-user-roles/" + "{userId}";
            public const string GetRolesList = Prefix + "/roles";
            public const string GetClimsList = Prefix + "/claims";
            public const string MangeUserClaims = Prefix + "/manage-user-claims/" + "{userId}";
            public const string UpdateRole = Prefix + "/update-role";
            public const string UpdateClaims = Prefix + "/update-claims";
        }
    }
}
