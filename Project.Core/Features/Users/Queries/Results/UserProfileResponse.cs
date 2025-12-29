namespace Project.Core.Features.Users.Queries.Results
{
    public record UserProfileResponse
    (
       string Email,
       string FirstName,
       string LastName,
       string FullName,
       string UserName
        );
}
