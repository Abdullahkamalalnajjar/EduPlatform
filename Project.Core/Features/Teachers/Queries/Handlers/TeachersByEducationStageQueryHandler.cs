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

            if (!teachers.Any())
                return NotFound<IEnumerable<TeacherByGradeSubjectResponse>>("No teachers found for the specified education stage and subject");

            var result = teachers.Select(t => new TeacherByGradeSubjectResponse
            {
                Id = t.Id,
                UserId = t.ApplicationUserId,
                ApplicationUserId = t.ApplicationUserId,
                FullName = t.User?.FullName ?? "Unknown",
                PhoneNumber = t.PhoneNumber,
                PhotoUrl = t.PhotoUrl,
                SubjectId = t.SubjectId,
                SubjectName = t.Subject?.Name ?? "Unknown",
                TeacherProfile = t.PhotoUrl,
                WhatAppNumber = t.WhatsAppNumber,
                FacebookUrl = t.FacebookUrl,
                TelegramUrl = t.TelegramUrl,
                Courses = t.Courses?.Where(c => c != null).Select(c => new CourseDtoo
                {
                    Id = c.Id,
                    Title = c.Title,
                    EducationStageName = c.EducationStage?.Name ?? "Unknown",
                    CourseImageUrl = c.CourseImageUrl,
                    Price = c.Price,
                    DiscountedPrice = c.DiscountedPrice,
                    Lectures = c.Lectures?.Where(l => l != null).Select(l => new LectureDtoo
                    {
                        Id = l.Id,
                        Title = l.Title,
                        Materials = l.Materials?.Where(m => m != null).Select(m => new MaterialDtoo
                        {
                            Id = m.Id,
                            Type = m.Type,
                            FileUrl = m.FileUrl,
                            IsFree = m.IsFree
                        }).ToList() ?? new List<MaterialDtoo>()
                    }).ToList() ?? new List<LectureDtoo>()
                }).ToList() ?? new List<CourseDtoo>()
            }).ToList();

            return Success<IEnumerable<TeacherByGradeSubjectResponse>>(result);
        }
    }
}
