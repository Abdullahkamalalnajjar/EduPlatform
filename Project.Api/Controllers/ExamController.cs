using Microsoft.AspNetCore.Mvc;
using Project.Api.Base;
using Project.Core.Features.Exams.Commands.Models;
using Project.Core.Features.Exams.Queries.Models;
using Project.Data.AppMetaData;

namespace Project.Api.Controllers
{
    public class ExamController : AppBaseController
    {
        [HttpGet(Router.ExamRouting.List)]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetAllExamsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.ExamRouting.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetExamByIdQuery { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpGet(Router.LectureRouting.List + "/{lectureId}/exam")]
        public async Task<IActionResult> GetByLectureId(int lectureId)
        {
            var request = new GetExamByLectureIdQuery { LectureId = lectureId };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(Router.ExamRouting.Create)]
        public async Task<IActionResult> Create([FromBody] CreateExamCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.ExamRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditExamCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.ExamRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteExamCommand { Id = id };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        // Submit exam answers
        [HttpPost(Router.ExamRouting.List + "/submit")]
        public async Task<IActionResult> SubmitAnswers([FromBody] SubmitExamAnswersCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        // Get student's score for an exam
        [HttpGet(Router.ExamRouting.List + "/{examId}/students/{studentId}/score")]
        public async Task<IActionResult> GetStudentScore(int examId, int studentId)
        {
            var request = new GetStudentExamScoreQuery { ExamId = examId, StudentId = studentId };
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
