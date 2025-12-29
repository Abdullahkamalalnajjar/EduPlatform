
using Project.Data.Entities.Users;

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

                // Create a new user
                var newUser = _mapper.Map<ApplicationUser>(request);
                var result = await _userManager.CreateAsync(newUser, request.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest<string>(errors);
                }

                // Generate confirmation token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                // Send confirmation email
                await SendConfirmationEmail(newUser, code);

                // Commit the transaction
                await transaction.CommitAsync();

                return Success("Account created successfully. Please check your email to confirm your account.", "User created successfully");
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of any failure
                await transaction.RollbackAsync();
                // Log the exception if needed (not shown here)
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






