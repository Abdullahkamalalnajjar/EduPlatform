using AutoMapper;
using MediatR;
using Project.Core.Features.pepole.Student.Queries.Models;
using Project.Core.Features.pepole.Student.Queries.Ruselt;
using Project.Data.Interfaces;

namespace Project.Core.Features.pepole.Student.Queries.Handlers
{
    public class StudentRuseltQueryHandler :
        IRequestHandler<GetStudentRuseltByIdQuery, StudentRuselt>,
        IRequestHandler<GetAllStudentRuseltQuery, IEnumerable<StudentRuselt>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentRuseltQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<StudentRuselt> Handle(GetStudentRuseltByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);
            if (student == null) throw new KeyNotFoundException("Student not found");

            return _mapper.Map<StudentRuselt>(student);
        }

        public async Task<IEnumerable<StudentRuselt>> Handle(GetAllStudentRuseltQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentRuselt>>(students);
        }
    }
}