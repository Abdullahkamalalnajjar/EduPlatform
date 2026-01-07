using Project.Core.Features.Teachers.Queries.Models;
using Project.Core.Features.Teachers.Queries.Results;

namespace Project.Core.Features.Teachers.Queries.Handlers
{
    public class TeacherCourseRevenueQueryHandler : ResponseHandler,
        IRequestHandler<GetTeacherCourseRevenueQuery, Response<TeacherCourseRevenueResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherCourseRevenueQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<TeacherCourseRevenueResponse>> Handle(GetTeacherCourseRevenueQuery request, CancellationToken cancellationToken)
        {
            // Get teacher details
            var teacher = await _unitOfWork.Teachers.GetTableNoTracking()
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == request.TeacherId, cancellationToken);

            if (teacher is null)
                return NotFound<TeacherCourseRevenueResponse>("Teacher not found");

            // Get all approved course subscriptions for this teacher's courses
            var approvedSubscriptions = await _unitOfWork.CourseSubscriptions.GetTableNoTracking()
                .Include(cs => cs.Course)
                .Include(cs => cs.Student)
                .ThenInclude(s => s.User)
                .Where(cs => cs.Course.TeacherId == request.TeacherId && cs.Status == "Approved")
                .ToListAsync(cancellationToken);

            // Group by course and calculate revenue
            var courseRevenues = approvedSubscriptions
                .GroupBy(cs => cs.Course)
                .Select(g => new CourseRevenueDetail
                {
                    CourseId = g.Key.Id,
                    CourseTitle = g.Key.Title,
                    CoursePrice = g.Key.Price,
                    ApprovedSubscriptions = g.Count(),
                    CourseRevenue = (g.Key.Price ?? 0) * g.Count(),
                    Students = g.Select(cs => new StudentRevenueDetail
                    {
                        StudentId = cs.Student.Id,
                        StudentName = cs.Student.User.FullName,
                        StudentEmail = cs.Student.User.Email,
                        PaidAmount = cs.Course.Price ?? 0,
                        SubscriptionDate = cs.CreatedAt
                    }).ToList()
                })
                .ToList();

            var totalRevenue = courseRevenues.Sum(cr => cr.CourseRevenue);
            var totalApprovedSubscriptions = courseRevenues.Sum(cr => cr.ApprovedSubscriptions);

            var response = new TeacherCourseRevenueResponse
            {
                TeacherId = request.TeacherId,
                TeacherName = teacher.User.FullName,
                Courses = courseRevenues,
                TotalRevenue = totalRevenue,
                TotalApprovedSubscriptions = totalApprovedSubscriptions
            };

            return Success(response);
        }
    }
}
