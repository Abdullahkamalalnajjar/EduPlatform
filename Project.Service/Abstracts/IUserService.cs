using Project.Data.Entities.Users;

namespace Project.Service.Abstracts
{
    public interface IUserService
    {
        public Task<ApplicationUser> GetUserProfileAsync(string userId);
        public Task<IEnumerable<UserResponse>> GetAllUsers();
        public Task<UserResponse> GetUserById(string id);
        public Task<string> UpdateProfileUser(ApplicationUser user);
        public Task<string> CreateUserAsync(string email, string firstName, string LastName, string password, IList<string> roles);
        public Task<string> UpdateUserAsync(ApplicationUser user, IList<string> roles);


    }
}
