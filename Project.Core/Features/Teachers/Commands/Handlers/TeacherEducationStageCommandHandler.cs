using MediatR;
using Project.Core.Features.Teachers.Commands.Models;
using Project.Data.Entities.People;
using Project.Data.Interfaces;

namespace Project.Core.Features.Teachers.Commands.Handlers
{
    public class TeacherEducationStageCommandHandler : ResponseHandler,
        IRequestHandler<AddTeacherEducationStageCommand, Response<int>>,
        IRequestHandler<RemoveTeacherEducationStageCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherEducationStageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(AddTeacherEducationStageCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(request.TeacherId);
            if (teacher is null) return NotFound<int>("Teacher not found");

            // prevent duplicate
            var exists = await _unitOfWork.TeacherEducationStages.GetTableNoTracking()
                .AnyAsync(ts => ts.TeacherId == request.TeacherId && ts.EducationStageId == request.EducationStageId, cancellationToken);
            if (exists) return BadRequest<int>("Teacher already assigned to this education stage");

            var ts = new TeacherEducationStage { TeacherId = request.TeacherId, EducationStageId = request.EducationStageId };
            await _unitOfWork.TeacherEducationStages.AddAsync(ts, cancellationToken);
            await _unitOfWork.CompeleteAsync();
            return Success(ts.Id);
        }

        public async Task<Response<string>> Handle(RemoveTeacherEducationStageCommand request, CancellationToken cancellationToken)
        {
            var ts = await _unitOfWork.TeacherEducationStages.GetByIdAsync(request.Id);
            if (ts is null) return NotFound<string>("Assignment not found");
            await _unitOfWork.TeacherEducationStages.Delete(ts);
            await _unitOfWork.CompeleteAsync();
            return Success("Deleted");
        }
    }
}
