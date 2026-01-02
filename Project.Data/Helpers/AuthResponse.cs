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
        string? WhatsAppNumber,
        string? PhotoUrl
 );
}
