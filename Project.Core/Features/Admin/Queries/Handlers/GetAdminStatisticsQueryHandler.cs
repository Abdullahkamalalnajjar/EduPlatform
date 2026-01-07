using Microsoft.EntityFrameworkCore;
using Project.Data.Dtos;
using Project.Data.Interfaces;
using Project.Core.Features.Admin.Queries.Models;

namespace Project.Core.Features.Admin.Queries.Handlers
{
    public class GetAdminStatisticsQueryHandler : ResponseHandler, IRequestHandler<GetAdminStatisticsQuery, Response<AdminStatisticsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAdminStatisticsQueryHandler> _logger;

        public GetAdminStatisticsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAdminStatisticsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<AdminStatisticsResponse>> Handle(GetAdminStatisticsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Get current statistics
                var totalTeachers = await _unitOfWork.Teachers.GetTableNoTracking().CountAsync(cancellationToken);
                var totalStudents = await _unitOfWork.Students.GetTableNoTracking().CountAsync(cancellationToken);
                var totalParents = await _unitOfWork.Parents.GetTableNoTracking().CountAsync(cancellationToken);
                var totalExams = await _unitOfWork.Exams.GetTableNoTracking().CountAsync(cancellationToken);
                var totalCourses = await _unitOfWork.Courses.GetTableNoTracking().CountAsync(cancellationToken);
                var totalUsers = await _unitOfWork.Users.GetTableNoTracking().CountAsync(cancellationToken);

                // Get this month's new records (created in the last 30 days)
                var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

                // For teachers and students, we don't have creation date, so we'll estimate
                // You may need to add CreatedAt field to these entities if you want accurate counts
                var newTeachersThisMonth = 0;
                var newStudentsThisMonth = 0;
                var newParentsThisMonth = 0;
                var newExamsThisMonth = await _unitOfWork.Exams.GetTableNoTracking()
                    .CountAsync(e => e.Deadline.HasValue && e.Deadline.Value >= thirtyDaysAgo, cancellationToken);
                var newCoursesThisMonth = 0;

                // Build response
                var response = new AdminStatisticsResponse
                {
                    TotalTeachers = totalTeachers,
                    TotalStudents = totalStudents,
                    TotalParents = totalParents,
                    TotalExams = totalExams,
                    TotalCourses = totalCourses,
                    TotalRegisteredUsers = totalUsers,
                    TeachersChangeMessage = $"+{newTeachersThisMonth} this month",
                    StudentsChangeMessage = $"+{newStudentsThisMonth} students",
                    ParentsChangeMessage = $"+{newParentsThisMonth} new parent",
                    ExamsChangeMessage = $"+{newExamsThisMonth} exams",
                    CoursesChangeMessage = $"+{newCoursesThisMonth} courses",
                    RegisteredUsersChangeMessage = $"+{newTeachersThisMonth + newStudentsThisMonth + newParentsThisMonth} this month"
                };

                return Success(response, "Admin statistics retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving admin statistics");
                return BadRequest<AdminStatisticsResponse>($"Error retrieving statistics: {ex.Message}");
            }
        }
    }
}
