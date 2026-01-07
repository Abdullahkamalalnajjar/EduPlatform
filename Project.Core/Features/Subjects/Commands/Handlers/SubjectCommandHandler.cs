using Project.Core.Features.Subjects.Commands.Models;
using Project.Data.Entities.Curriculum;

namespace Project.Core.Features.Subjects.Commands.Handlers
{
    public class SubjectCommandHandler : ResponseHandler,
        IRequestHandler<CreateSubjectCommand, Response<int>>,
        IRequestHandler<EditSubjectCommand, Response<int>>,
        IRequestHandler<DeleteSubjectCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public SubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Response<int>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = new Subject { Name = request.Name };

            // Handle image upload if provided
            if (request.SubjectImageFile != null && request.SubjectImageFile.Length > 0)
            {
                var imageUrl = await _fileService.UploadImage("uploads/subjects", request.SubjectImageFile);
                if (string.IsNullOrEmpty(imageUrl) || imageUrl == "FailedToUploadImage" || imageUrl == "NoImage")
                {
                    return BadRequest<int>("Failed to upload subject image");
                }
                subject.SubjectImageUrl = imageUrl;
            }

            await _unitOfWork.Subjects.AddAsync(subject, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return Success(subject.Id);
        }

        public async Task<Response<int>> Handle(EditSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(request.Id);
            if (subject is null) return NotFound<int>("Subject not found");
            
            // Update subject name
            subject.Name = request.Name;

            // Handle image upload if provided
            if (request.SubjectImageFile != null && request.SubjectImageFile.Length > 0)
            {
                var imageUrl = await _fileService.UploadImage("uploads/subjects", request.SubjectImageFile);
                if (string.IsNullOrEmpty(imageUrl) || imageUrl == "FailedToUploadImage" || imageUrl == "NoImage")
                {
                    return BadRequest<int>("Failed to upload subject image");
                }
                subject.SubjectImageUrl = imageUrl;
            }

            _unitOfWork.Subjects.Update(subject);
            await _unitOfWork.CompeleteAsync();
            return Success(subject.Id);
        }

        public async Task<Response<string>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(request.Id);
            if (subject is null) 
                return NotFound<string>("Subject not found");

            // Check if subject has any teachers assigned
            var teachersWithSubject = await _unitOfWork.Teachers.GetTableNoTracking()
                .Where(t => t.SubjectId == request.Id)
                .CountAsync(cancellationToken);

            if (teachersWithSubject > 0)
                return BadRequest<string>($"Cannot delete subject. There are {teachersWithSubject} teacher(s) assigned to this subject. Please reassign or delete the teachers first.");

            // Subject can be safely deleted
            await _unitOfWork.Subjects.Delete(subject);
            await _unitOfWork.CompeleteAsync();
            return Success("Subject deleted successfully");
        }
    }
}