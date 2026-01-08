namespace Project.Data.Helpers
{
    public record AuthResponse
    (
        string Id,
        string? Email,
        string FirstName,
        string LastName,
        string Token,
        bool IsDisable,
        int TokenExpiresIn,
        string? RefreshToken,
        DateTime RefreshTokenExpiresIn,
        IEnumerable<string> Roles,
        int? UserId,
        // teacher profile fields (nullable for non-teacher users)
        string? PhoneNumber,
        string? FacebookUrl,
        string? TelegramUrl,
        string? YouTubeChannelUrl,
        string? WhatsAppNumber,
        string? PhotoUrl,
        // assistant teacher id (nullable for non-assistant users)
        int? TeacherId,
        // تنبيه المستخدم عند تسجيل الدخول من جهاز جديد
        string? PreviousDeviceWarning = null // تحذير: تم تسجيل الخروج من الجهاز السابق
 );
}
