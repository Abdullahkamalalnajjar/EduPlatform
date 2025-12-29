using AutoMapper;
using MediatR;
using Project.Core.Features.Courses.Commands.Models;
using Project.Service.Abstracts;
using Project.Data.Entities.Curriculum;

namespace Project.Core.Features.Courses.Commands.Handlers
{
    public class CourseCommandHandler : ResponseHandler,
        IRequestHandler<CreateCourseCommand, Response<int>>,
        IRequestHandler<EditCourseCommand, Response<int>>,
        IRequestHandler<DeleteCourseCommand, Response<string>>
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseCommandHandler(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course { Title = request.Title, GradeYear = request.GradeYear, TeacherId = request.TeacherId };
            var created = await _courseService.CreateAsync(course, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetByIdAsync(request.Id, cancellationToken);
            if (course is null) return NotFound<int>("Course not found");
            course.Title = request.Title;
            course.GradeYear = request.GradeYear;
            course.TeacherId = request.TeacherId;
            var updated = await _courseService.UpdateAsync(course, cancellationToken);
            return Success(updated.Id);
        }

        public async Task<Response<string>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetByIdAsync(request.Id, cancellationToken);
            if (course is null) return NotFound<string>("Course not found");
            await _courseService.DeleteAsync(request.Id, cancellationToken);
            return Success("Deleted");
        }
    }
}