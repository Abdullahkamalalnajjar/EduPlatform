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

        public SubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = new Subject { Name = request.Name };
            await _unitOfWork.Subjects.AddAsync(subject, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return Success(subject.Id);
        }

        public async Task<Response<int>> Handle(EditSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(request.Id);
            if (subject is null) return NotFound<int>("Subject not found");
            subject.Name = request.Name;
            _unitOfWork.Subjects.Update(subject);
            await _unitOfWork.CompeleteAsync();
            return Success(subject.Id);
        }

        public async Task<Response<string>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(request.Id);
            if (subject is null) return NotFound<string>("Subject not found");
            await _unitOfWork.Subjects.Delete(subject);
            await _unitOfWork.CompeleteAsync();
            return Success("Deleted");
        }
    }
}