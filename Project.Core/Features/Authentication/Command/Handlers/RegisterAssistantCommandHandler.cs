using Project.Data.Entities.People;
using Project.Data.Entities.Users;

namespace Project.Core.Features.Authentication.Command.Handlers
{
    public class RegisterAssistantCommandHandler : ResponseHandler, IRequestHandler<RegisterAssistantCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegisterAssistantCommandHandler> _logger;

        public RegisterAssistantCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, ILogger<RegisterAssistantCommandHandler> logger)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(RegisterAssistantCommand request, CancellationToken cancellationToken)
        {
            // Validate teacher exists
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(request.TeacherId);
            if (teacher == null)
                return NotFound<string>("Teacher not found");

            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return BadRequest<string>("Email already registered");

            // Create new user
            var newUser = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                _logger.LogError($"Failed to create assistant user: {errors}");
                return BadRequest<string>($"Failed to create user: {errors}");
            }

            // Assign Assistant role
            var roleResult = await _userManager.AddToRoleAsync(newUser, "Assistant");
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                _logger.LogError($"Failed to assign Assistant role: {errors}");
                return BadRequest<string>($"Failed to assign role: {errors}");
            }

            // Create Assistant profile linked to teacher
            var assistant = new Assistant
            {
                ApplicationUserId = newUser.Id,
                TeacherId = request.TeacherId
            };

            await _unitOfWork.Assistants.AddAsync(assistant, cancellationToken);
            await _unitOfWork.CompeleteAsync();

            _logger.LogInformation($"Assistant account created successfully for teacher {request.TeacherId}");

            return Success<string>("Assistant account created successfully", new { UserId = newUser.Id, Email = newUser.Email });
        }
    }
}
