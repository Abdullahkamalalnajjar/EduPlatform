using Project.Data.Entities.Users;
using Project.Data.Entities.People;
using Project.Data.Entities.Curriculum;

namespace Project.Core.Features.Authentication.Command.Handlers
{
    public class AuthenticatoinCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<AuthResponse>>,
        IRequestHandler<SignUpUserCommand, Response<string>>,
        IRequestHandler<CreateNewRefreshTokenCommand, Response<AuthResponse>>,
        IRequestHandler<RevokeRefreashTokenCommand, Response<string>>,
        IRequestHandler<ConfirmEmailCommand, Response<string>>,
        IRequestHandler<ResendConfirmEmailCommand, Response<string>>

    {
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticatoinCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticatoinCommandHandler(IEmailSender emailSender, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IAuthService authService, ILogger<AuthenticatoinCommandHandler> logger, IMapper mapper, IJwtProvider jwtProvider, UserManager<ApplicationUser> userManager)
        {
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
            _logger = logger;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
            _userManager = userManager;
        }
        public async Task<Response<AuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                if (result.ErrorMessage == "EmailNotConfirmed")
                    return BadRequest<AuthResponse>("Email is not confirmed. Please confirm your email first.");
                return BadRequest<AuthResponse>(result.ErrorMessage);
            }

            return Success(result.Response!, "Login successful");
        }

        public async Task<Response<string>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Check if the user already exists
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser is not null)
                    return BadRequest<string>("An account with this email already exists.");

                // Create a new user and mark email confirmed so no confirmation required
                var newUser = _mapper.Map<ApplicationUser>(request);
                newUser.EmailConfirmed = true; // skip email confirmation

                var result = await _userManager.CreateAsync(newUser, request.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest<string>(errors);
                }

                // assign role
                var role = request.Role ?? "Student";
                var roleResult = await _userManager.AddToRoleAsync(newUser, role);
                if (!roleResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return BadRequest<string>("Failed to assign role to user.");
                }

                // create profile based on role
                switch (role.ToLowerInvariant())
                {
                    case "student":
                        var student = new Student
                        {
                            ApplicationUserId = newUser.Id,
                            GradeYear = request.GradeYear ?? 0,
                            ParentPhoneNumber = request.ParentPhoneNumber ?? string.Empty
                        };
                        await _unitOfWork.Students.AddAsync(student, cancellationToken);
                        break;
                    case "teacher":
                        if (!request.SubjectId.HasValue)
                        {
                            await transaction.RollbackAsync();
                            return BadRequest<string>("SubjectId is required for teacher role.");
                        }
                        var teacher = new Teacher
                        {
                            ApplicationUserId = newUser.Id,
                            SubjectId = request.SubjectId.Value
                        };
                        await _unitOfWork.Teachers.AddAsync(teacher, cancellationToken);
                        break;
                    case "parent":
                        var parent = new Parent
                        {
                            ApplicationUserId = newUser.Id,
                            NationalId = request.NationalId ?? string.Empty
                        };
                        await _unitOfWork.Parents.AddAsync(parent, cancellationToken);
                        break;
                    case "assistant":
                        if (!request.TeacherId.HasValue)
                        {
                            await transaction.RollbackAsync();
                            return BadRequest<string>("TeacherId is required for assistant role.");
                        }
                        var assistant = new Assistant
                        {
                            ApplicationUserId = newUser.Id,
                            TeacherId = request.TeacherId.Value
                        };
                        await _unitOfWork.Assistants.AddAsync(assistant, cancellationToken);
                        break;
                    default:
                        break;
                }

                // persist changes
                await _unitOfWork.CompeleteAsync();
                await transaction.CommitAsync();

                // generate token so user is logged in immediately
                var tokenResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
                if (!string.IsNullOrEmpty(tokenResult.ErrorMessage))
                {
                    // user created but token generation failed
                    return Success<string>("", "Account created but automatic login failed: " + tokenResult.ErrorMessage);
                }

                // return token value to client
                return Success<string>(tokenResult.Response!.Token, "Account created and logged in successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest<string>(ex.Message.ToString());
            }
        }

        public async Task<Response<AuthResponse>> Handle(CreateNewRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var newRefreshToken = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken);
            if (newRefreshToken is null)
                return BadRequest<AuthResponse>("Invalid token or refresh token");
            return Success<AuthResponse>(newRefreshToken, "Refresh token created successfully");

        }

        public async Task<Response<string>> Handle(RevokeRefreashTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken);
            if (!result)
                return BadRequest<string>("Invalid token or refresh token");
            return Success<string>("Refresh token revoked successfully", "Refresh token revoked successfully");
        }

        public async Task<Response<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.ConfirmEmailAsync(request.UserId, request.Code);

            return result switch
            {
                "Email confirmed successfully" => Success<string>("Email Confirm Successfully"),
                "Email already confirmed" => BadRequest<string>("Email already confirmed"),
                "InvalidCode" => BadRequest<string>("Invalid confirmation code."),
                _ => BadRequest<string>($"Email confirmation failed: {result}")
            };
        }

        public async Task<Response<string>> Handle(ResendConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.ResendConfirmEmailAsync(request.Email);

            return result switch
            {
                "Good" => Success(""),
                "Daplicated Confirmation" => BadRequest<string>("Daplicated Confirmation"),
                "Code Has been resend" => Success("Code has been resend to Confirmation"),
                _ => BadRequest<string>("Resend Code Failed")
            };
        }
        private async Task SendConfirmationEmail(ApplicationUser user, string code)
        {
            // استخدام عنوان الباكيند مباشرة
            var request = _httpContextAccessor.HttpContext?.Request;
            var origin = $"{request?.Scheme}://{request?.Host}";

            // توليد رابط تأكيد البريد
            var encodedCode = HttpUtility.UrlEncode(code);
            var confirmationUrl = $"{origin}/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={encodedCode}";

            // إنشاء جسم الإيميل
            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                new Dictionary<string, string> {
            {"{{name}}" , user.FullName },
            {"{{action_url}}" , confirmationUrl }
                });

            await _emailSender.SendEmailAsync(user.Email!, "AGECS Licensing: تأكيد البريد الإلكتروني", emailBody);
        }

    }
}





















