using AutoMapper;
using MediatR;
using Project.Core.Features.Lectures.Commands.Models;
using Project.Service.Abstracts;
using Project.Data.Entities.Content;

namespace Project.Core.Features.Lectures.Commands.Handlers
{
    public class LectureCommandHandler : ResponseHandler,
        IRequestHandler<CreateLectureCommand, Response<int>>,
        IRequestHandler<EditLectureCommand, Response<int>>,
        IRequestHandler<DeleteLectureCommand, Response<string>>
    {
        private readonly ILectureService _lectureService;
        private readonly IMapper _mapper;

        public LectureCommandHandler(ILectureService lectureService, IMapper mapper)
        {
            _lectureService = lectureService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateLectureCommand request, CancellationToken cancellationToken)
        {
            var lecture = new Lecture { Title = request.Title, CourseId = request.CourseId };
            var created = await _lectureService.CreateAsync(lecture, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditLectureCommand request, CancellationToken cancellationToken)
        {
            var lecture = await _lectureService.GetByIdAsync(request.Id, cancellationToken);
            if (lecture is null) return NotFound<int>("Lecture not found");
            lecture.Title = request.Title;
            lecture.CourseId = request.CourseId;
            var updated = await _lectureService.UpdateAsync(lecture, cancellationToken);
            return Success(updated.Id);
        }

        public async Task<Response<string>> Handle(DeleteLectureCommand request, CancellationToken cancellationToken)
        {
            var lecture = await _lectureService.GetByIdAsync(request.Id, cancellationToken);
            if (lecture is null) return NotFound<string>("Lecture not found");
            await _lectureService.DeleteAsync(request.Id, cancellationToken);
            return Success("Deleted");
        }
    }
}