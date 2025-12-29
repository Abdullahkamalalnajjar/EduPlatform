namespace Project.Data.Dtos
{
    public record UserResponse
    (string Id, string Email, string FirstName, string LastName, IEnumerable<string> Roles);
}
