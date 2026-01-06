using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Project.Api.Base;
using Project.Core.Features.Parents.Queries.Models;

namespace Project.Api.Controllers
{
    [Authorize]
    public class ParentController : AppBaseController
    {
        [HttpGet("MyStudents/{parentId}")]
        public async Task<IActionResult> GetMyStudents(int parentId)
        {
            var request = new GetStudentsByParentIdQuery { ParentId = parentId };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpGet("Student/{studentId}/Course/{courseId}/ExamScores")]
        public async Task<IActionResult> GetStudentCourseExamScores(int studentId, int courseId)
        {
            var request = new GetStudentCourseExamScoresQuery { StudentId = studentId, CourseId = courseId };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
