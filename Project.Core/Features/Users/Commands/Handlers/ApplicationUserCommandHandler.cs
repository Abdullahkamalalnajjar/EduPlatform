using Project.Data.Entities.Users;

namespace Project.Core.Features.Users.Commands.Handlers
{
    public class ApplicationUserCommandHandler(IUserService userService, ICurrentUserService currentUserService, IMapper mapper, ApplicationDbContext context, IRoleService roleService, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IFileService fileService) : ResponseHandler,
        IRequestHandler<EditApplicationUserCommand, Response<string>>,
        IRequestHandler<CreateUserCommand, Response<string>>,
        IRequestHandler<UpdateUserCommand, Response<string>>,
        IRequestHandler<ApproveTeacherAccountCommand, Response<string>>

    {
        private readonly IUserService _userService = userService;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _context = context;
        private readonly IRoleService _roleService = roleService;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IFileService _fileService = fileService;

        #region EditProfile
        public async Task<Response<string>> Handle(EditApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound<string>("User not found");

            // Update basic info
            if (!string.IsNullOrWhiteSpace(request.FirstName))
                user.FirstName = request.FirstName;
            
            if (!string.IsNullOrWhiteSpace(request.LastName))
                user.LastName = request.LastName;

            // Update user
            var userResult = await _userService.UpdateProfileUser(user);
            if (userResult != "Updated")
                return UnprocessableEntity<string>("Failed to update user profile");

            // Get user role to update teacher profile if needed
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains("Teacher"))
            {
                var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.ApplicationUserId == request.UserId, cancellationToken);
                if (teacher != null)
                {
                    // Update teacher profile URLs
                    if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                        teacher.PhoneNumber = request.PhoneNumber;
                    
                    if (!string.IsNullOrWhiteSpace(request.FacebookUrl))
                        teacher.FacebookUrl = request.FacebookUrl;
                    
                    if (!string.IsNullOrWhiteSpace(request.TelegramUrl))
                        teacher.TelegramUrl = request.TelegramUrl;
                    
                    if (!string.IsNullOrWhiteSpace(request.YouTubeChannelUrl))
                        teacher.YouTubeChannelUrl = request.YouTubeChannelUrl;
                    
                    if (!string.IsNullOrWhiteSpace(request.WhatsAppNumber))
                        teacher.WhatsAppNumber = request.WhatsAppNumber;

                    // Handle photo file upload if provided
                    if (request.PhotoFile != null && request.PhotoFile.Length > 0)
                    {
                        var photoUrl = await _fileService.UploadImage("uploads/teachers", request.PhotoFile);
                        if (!string.IsNullOrEmpty(photoUrl) && photoUrl != "FailedToUploadImage" && photoUrl != "NoImage")
                        {
                            teacher.PhotoUrl = photoUrl;
                        }
                        else
                        {
                            return BadRequest<string>("Failed to upload profile photo");
                        }
                    }

                    _unitOfWork.Teachers.Update(teacher);
                    await _unitOfWork.CompeleteAsync();
                }
            }

            return Updated<string>("User profile has been updated successfully");
        }
        #endregion


        public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var emailIsExist = await _unitOfWork.Users.GetTableNoTracking().AnyAsync(x => x.Email == request.Email);
            if (emailIsExist) return UnprocessableEntity<string>("Email already exists");
            var allowRoles = await _roleService.GetAllRolesAsync();
            if (allowRoles is null) return UnprocessableEntity<string>("No roles available, please create roles first");
            if (request.Roles.Except(allowRoles.Select(x => x.Name)).Any())
                return UnprocessableEntity<string>("One or more roles are not valid, please check the roles provided");
            var result = await _userService.CreateUserAsync(request.Email, request.FirstName, request.LastName, request.Password, request.Roles);
            if (result == "Created")
                return Created<string>("Application user has been created successfully", new { request.Email });
            if (result == "ErrorCreatingUser")
                return UnprocessableEntity<string>("Error creating user, please try again");
            if (result == "UnknownError")
                return UnprocessableEntity<string>(result);
            return UnprocessableEntity<string>("An unknown error occurred while creating the user");
        }

        #region EditUserWithRole
        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return NotFound<string>("User not found");
            var allowRoles = await _roleService.GetAllRolesAsync();
            if (allowRoles is null) return UnprocessableEntity<string>("No roles available, please create roles first");
            if (request.Roles.Except(allowRoles.Select(x => x.Name)).Any())
                return UnprocessableEntity<string>("One or more roles are not valid, please check the roles provided");
            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToRemove = currentRoles.Except(request.Roles).ToList();
            await _context.Roles.Where(x => x.Id == user.Id).ExecuteDeleteAsync();
            _mapper.Map(request, user);
            var result = await _userService.UpdateUserAsync(user, request.Roles);
            if (result == "Updated")
                return Updated<string>("Application user has been updated successfully", new { request.UserId });
            return UnprocessableEntity<string>("Exit error when make update");
        }

        #endregion

        #region ApproveTeacher
        public async Task<Response<string>> Handle(ApproveTeacherAccountCommand request, CancellationToken cancellationToken)
        {
            // Find the teacher user
            var user = await _userManager.FindByIdAsync(request.TeacherUserId);
            if (user == null)
                return NotFound<string>("Teacher user not found");

            // Check if user is a teacher
            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Contains("Teacher"))
                return BadRequest<string>("This user is not a teacher");

            // Find the teacher profile
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.ApplicationUserId == request.TeacherUserId, cancellationToken);
            
            if (teacher == null)
                return NotFound<string>("Teacher profile not found");

            // Update teacher account status based on IsApproved flag
            user.IsDisable = !request.IsApproved;  // Set IsDisable to opposite of IsApproved
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest<string>($"Failed to update teacher account: {errors}");
            }

            var statusMessage = request.IsApproved 
                ? "Teacher account approved and enabled successfully" 
                : "Teacher account disabled successfully";

            return Success<string>(statusMessage);
        }
        #endregion

    }
}
