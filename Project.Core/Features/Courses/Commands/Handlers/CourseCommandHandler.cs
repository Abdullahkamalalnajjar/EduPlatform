using Project.Core.Features.Courses.Commands.Models;
using Project.Data.Entities.Curriculum;

namespace Project.Core.Features.Courses.Commands.Handlers
{
    public class CourseCommandHandler : ResponseHandler,
        IRequestHandler<CreateCourseCommand, Response<int>>,
        IRequestHandler<EditCourseCommand, Response<int>>,
        IRequestHandler<DeleteCourseCommand, Response<string>>
    {
        private readonly ICourseService _courseService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CourseCommandHandler(ICourseService courseService, IFileService fileService, IMapper mapper)
        {
            _courseService = courseService;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var imageUrl = string.Empty;
            if (request.CourseImageUrl != null)
            {
                imageUrl = await _fileService.UploadImage("courses", request.CourseImageUrl);
                if (string.IsNullOrWhiteSpace(imageUrl) || imageUrl == "FailedToUploadImage" || imageUrl == "NoImage")
                {
                    return BadRequest<int>("Failed to upload course image");
                }
            }

            var course = new Course
            {
                Title = request.Title,
                TeacherId = request.TeacherId,
                EducationStageId = request.EducationStageId,
                CourseImageUrl = imageUrl,
                Price = request.Price,
                DiscountedPrice = request.DiscountedPrice
            };
            var created = await _courseService.CreateAsync(course, cancellationToken);
            return Success(created.Id);
        }

        public async Task<Response<int>> Handle(EditCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetByIdAsync(request.Id, cancellationToken);
            if (course is null) return NotFound<int>("Course not found");

            if (request.CourseImageUrl != null)
            {
                var imageUrl = await _fileService.UploadImage("courses", request.CourseImageUrl);
                if (string.IsNullOrWhiteSpace(imageUrl) || imageUrl == "FailedToUploadImage" || imageUrl == "NoImage")
                {
                    return BadRequest<int>("Failed to upload course image");
                }
                course.CourseImageUrl = imageUrl;
            }

            course.Title = request.Title;
            course.TeacherId = request.TeacherId;
            course.EducationStageId = request.EducationStageId;
            course.Price = request.Price;
            course.DiscountedPrice = request.DiscountedPrice;
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