using Project.Core.Features.Teachers.Queries.Models;
using Project.Core.Features.Teachers.Queries.Results;

namespace Project.Core.Features.Teachers.Queries.Handlers
{
    public class TeachersByEducationStageQueryHandler : ResponseHandler,
        IRequestHandler<GetTeachersByEducationStageSubjectQuery, Response<IEnumerable<TeacherByGradeSubjectResponse>>>
    {
        private readonly ITeacherService _teacherService;

        public TeachersByEducationStageQueryHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<Response<IEnumerable<TeacherByGradeSubjectResponse>>> Handle(GetTeachersByEducationStageSubjectQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _teacherService.GetByEducationStageAndSubjectAsync(request.EducationStageId, request.SubjectId, cancellationToken);

            var result = teachers.Select(t => new TeacherByGradeSubjectResponse
            {
                Id = t.Id,
                UserId = t.ApplicationUserId,
                ApplicationUserId = t.ApplicationUserId,
                FullName = t.User.FullName,

                SubjectId = t.SubjectId,
                SubjectName = t.Subject.Name,
                TeacherProfile = t.PhotoUrl,
                WhatAppNumber = t.WhatsAppNumber,
                Courses = t.Courses.Select(c => new CourseDtoo
                {
                    Id = c.Id,
                    Title = c.Title,
                    EducationStageName = c.EducationStage.Name,
                    Lectures = c.Lectures.Select(l => new LectureDtoo
                    {
                        Id = l.Id,
                        Title = l.Title,
                        Materials = l.Materials.Select(m => new MaterialDtoo
                        {
                            Id = m.Id,
                            Type = m.Type,
                            FileUrl = m.FileUrl,
                            IsFree = m.IsFree
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).ToList();

            return Success<IEnumerable<TeacherByGradeSubjectResponse>>(result);
        }
    }
}
