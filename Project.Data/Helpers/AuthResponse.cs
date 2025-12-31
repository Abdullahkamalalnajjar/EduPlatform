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
        string ApplicationUserId,
        int? UserId
 );
}
