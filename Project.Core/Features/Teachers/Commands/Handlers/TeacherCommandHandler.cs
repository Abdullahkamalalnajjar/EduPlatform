using AutoMapper;
using MediatR;
using Project.Core.Features.Teachers.Commands.Models;
using Project.Service.Abstracts;
using Project.Data.Entities.People;

namespace Project.Core.Features.Teachers.Commands.Handlers
{
    public class TeacherCommandHandler : ResponseHandler,
        IRequestHandler<CreateTeacherCommand, Response<int>>,
        IRequestHandler<EditTeacherCommand, Response<int>>
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public TeacherCommandHandler(ITeacherService teacherService, IMapper mapper, IFileService fileService)
        {
            _teacherService = teacherService;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Response<int>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            string? photoUrl = request.PhotoUrl;
            if (request.PhotoFile is not null)
            {
                var uploaded = await _fileService.UploadImage("uploads/teachers", request.PhotoFile);
                if (uploaded == "FailedToUploadImage" || uploaded == "NoImage")
                    return BadRequest<int>("Failed to upload photo");
                photoUrl = uploaded;
            }

            var teacher = new Teacher
            {
                SubjectId = request.SubjectId,
                PhoneNumber = request.PhoneNumber,
                FacebookUrl = request.FacebookUrl,
                TelegramUrl = request.TelegramUrl,
                WhatsAppNumber = request.WhatsAppNumber,
                PhotoUrl = photoUrl
            };
            var created = await _teacherService.CreateAsync(teacher, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherService.GetByIdAsync(request.Id, cancellationToken);
            if (teacher is null) return NotFound<int>("Teacher not found");

            string? photoUrl = request.PhotoUrl ?? teacher.PhotoUrl;
            if (request.PhotoFile is not null)
            {
                var uploaded = await _fileService.UploadImage("uploads/teachers", request.PhotoFile);
                if (uploaded == "FailedToUploadImage" || uploaded == "NoImage")
                    return BadRequest<int>("Failed to upload photo");
                photoUrl = uploaded;
            }

            teacher.SubjectId = request.SubjectId;
            teacher.PhoneNumber = request.PhoneNumber;
            teacher.FacebookUrl = request.FacebookUrl;
            teacher.TelegramUrl = request.TelegramUrl;
            teacher.WhatsAppNumber = request.WhatsAppNumber;
            teacher.PhotoUrl = photoUrl;
            var updated = await _teacherService.UpdateAsync(teacher, cancellationToken);
            return Success(updated.Id);
        }
    }
}
