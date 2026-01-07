using Project.Core.Features.Courses.Queries.Models;
using Project.Core.Features.Courses.Queries.Results;

namespace Project.Core.Features.Courses.Queries.Handlers
{
    public class CourseQueryHandler : ResponseHandler,
        IRequestHandler<GetAllCoursesQuery, Response<IEnumerable<CourseResponse>>>,
        IRequestHandler<GetCourseByIdQuery, Response<CourseResponse>>,
        IRequestHandler<GetCoursesByTeacherQuery, Response<IEnumerable<CourseDto>>>
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseQueryHandler(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CourseResponse>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetAllAsync(cancellationToken);
            var result = courses.Select(c => new CourseResponse
            {
                Id = c.Id,
                Title = c.Title,
                EducationStageName = c.EducationStage.Name,
                TeacherId = c.TeacherId,
                EducationStageId = c.EducationStageId,
                Price = c.Price,
                DiscountedPrice = c.DiscountedPrice
            }).ToList();
            return Success<IEnumerable<CourseResponse>>(result);
        }

        public async Task<Response<CourseResponse>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetByIdAsync(request.Id, cancellationToken);
            if (course is null) return NotFound<CourseResponse>("Course not found");
            var resp = new CourseResponse
            {
                Id = course.Id,
                Title = course.Title,
                EducationStageName = course.EducationStage.Name,
                TeacherId = course.TeacherId,
                EducationStageId = course.EducationStageId,
                Price = course.Price,
                DiscountedPrice = course.DiscountedPrice
            };
            return Success(resp);
        }

        public async Task<Response<IEnumerable<CourseDto>>> Handle(GetCoursesByTeacherQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetByTeacherIdAsync(request.TeacherId, cancellationToken);
            return Success<IEnumerable<CourseDto>>(courses);
        }
    }
}