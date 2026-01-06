namespace Project.Core.Features.Users.Queries.Handlers
{
    public class GetAllUsersQueryHandler : ResponseHandler, IRequestHandler<GetAllUsersQuery, Response<GetAllUsersResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetAllUsersResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Get all students
                var students = await _unitOfWork.Students.GetTableNoTracking()
                    .Include(s => s.User)
                    .Select(s => new UserStudentDto
                    {
                        StudentId = s.Id,
                        UserId = s.ApplicationUserId,
                        Email = s.User.Email!,
                        FirstName = s.User.FirstName!,
                        LastName = s.User.LastName!,
                        FullName = s.User.FullName,
                        GradeYear = s.GradeYear,
                        ParentPhoneNumber = s.ParentPhoneNumber
                    })
                    .ToListAsync(cancellationToken);

                // Get all teachers
                var teachers = await _unitOfWork.Teachers.GetTableNoTracking()
                    .Include(t => t.User)
                    .Include(t => t.Subject)
                    .Select(t => new UserTeacherDto
                    {
                        TeacherId = t.Id,
                        UserId = t.ApplicationUserId,
                        Email = t.User.Email!,
                        FirstName = t.User.FirstName!,
                        LastName = t.User.LastName!,
                        FullName = t.User.FullName,
                        PhoneNumber = t.PhoneNumber,
                        PhotoUrl = t.PhotoUrl,
                        SubjectId = t.SubjectId,
                        SubjectName = t.Subject.Name,
                        IsVerified = t.User.IsDisable
                    })
                    .ToListAsync(cancellationToken);

                // Get all parents
                var parents = await _unitOfWork.Parents.GetTableNoTracking()
                    .Include(p => p.User)
                    .Include(p => p.Children)
                    .Select(p => new UserParentDto
                    {
                        ParentId = p.Id,
                        UserId = p.ApplicationUserId,
                        Email = p.User.Email!,
                        FirstName = p.User.FirstName!,
                        LastName = p.User.LastName!,
                        FullName = p.User.FullName,
                        ParentPhoneNumber = p.ParentPhoneNumber,
                        ChildrenCount = p.Children.Count
                    })
                    .ToListAsync(cancellationToken);

                var response = new GetAllUsersResponse
                {
                    Students = students,
                    Teachers = teachers,
                    Parents = parents
                };

                return Success(response, "All users retrieved successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<GetAllUsersResponse>($"Error retrieving users: {ex.Message}");
            }
        }
    }
}
