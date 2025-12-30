using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Features.pepole.Student.Queries.Ruselt
{
    public class StudentRuselt
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = null!;
        public string UserName { get; set; } = null!; // يمكنك إضافة أي خصائص من User تحتاجها

        public int GradeYear { get; set; }
        public string ParentPhoneNumber { get; set; } = null!;

        // Parent Info (optional)
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }

        // Courses (optional)
        public List<CourseSubscriptionDto> CourseSubscriptions { get; set; } = new List<CourseSubscriptionDto>();
    }

    public class CourseSubscriptionDto
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
