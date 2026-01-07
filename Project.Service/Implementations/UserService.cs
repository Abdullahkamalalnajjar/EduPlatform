using Project.Data.Entities.Users;

namespace Project.Service.Implementations
{
    public class UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IRoleService roleService, ApplicationDbContext context) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IRoleService _roleService = roleService;
        private readonly ApplicationDbContext _context = context;

        public async Task<string> CreateUserAsync(string email, string firstName, string LastName, string password, IList<string> roles)
        {
            var user = new ApplicationUser
            {
                Email = email,
                FirstName = firstName,
                LastName = LastName,
                UserName = email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, roles);
                return "Created";
            }
            if (result.Errors.Any())
            {
                return result.Errors.FirstOrDefault()?.Description ?? "ErrorCreatingUser";
            }
            return "UnknownError";
        }

        public async Task<string> UpdateUserAsync(ApplicationUser user, IList<string> roles)
        {
            await _userManager.UpdateAsync(user);
            await _userManager.AddToRolesAsync(user, roles);
            return "Updated";

        }
        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await (from u in _context.Users
                               join ur in _context.UserRoles on u.Id equals ur.UserId
                               join r in _context.Roles on ur.RoleId equals r.Id
                               group r by new { u.Id, u.Email, u.FirstName, u.LastName } into g
                               //   where !g.Any(x => x.Name == DefaultRoles.Member)
                               select new UserResponse(
                                   g.Key.Id,
                                   g.Key.Email,
                                   g.Key.FirstName,
                                   g.Key.LastName,
                                   g.Select(x => x.Name)
                               )).ToListAsync();
            return users;
        }

        public async Task<UserResponse?> GetUserById(string id)
        {
            var user = await (from u in _context.Users
                              join ur in _context.UserRoles on u.Id equals ur.UserId
                              join r in _context.Roles on ur.RoleId equals r.Id
                              where u.Id == id
                              group r by new { u.Id, u.Email, u.FirstName, u.LastName } into g
                              select new UserResponse(
                                  g.Key.Id,
                                  g.Key.Email,
                                  g.Key.FirstName,
                                  g.Key.LastName,
                                  g.Select(r => r.Name).ToList()
                              )).SingleOrDefaultAsync();

            return user;
        }


        public async Task<ApplicationUser> GetUserProfileAsync(string userId)
            => await _unitOfWork.Users.GetTableNoTracking()
            .Where(x => x.Id.Equals(userId)).SingleAsync();
        public async Task<string> UpdateProfileUser(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
            return "Updated";
        }

        public async Task<string> DeleteUserAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return "UserNotFound";

                // Delete Student profile if exists
                var student = await _context.Students.FirstOrDefaultAsync(s => s.ApplicationUserId == userId);
                if (student != null)
                {
                    _context.Students.Remove(student);
                }

                // Delete Parent profile if exists
                var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == userId);
                if (parent != null)
                {
                    _context.Parents.Remove(parent);
                }

                // Delete Teacher profile if exists (this will cascade delete courses, lectures, etc.)
                var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.ApplicationUserId == userId);
                if (teacher != null)
                {
                    _context.Teachers.Remove(teacher);
                }

                // Delete Admin profile if exists
                var admin = await _context.Admins.FirstOrDefaultAsync(a => a.ApplicationUserId == userId);
                if (admin != null)
                {
                    _context.Admins.Remove(admin);
                }

                // Delete Assistant profile if exists
                var assistant = await _context.Assistants.FirstOrDefaultAsync(a => a.ApplicationUserId == userId);
                if (assistant != null)
                {
                    _context.Assistants.Remove(assistant);
                }

                // Save all deletions first
                await _context.SaveChangesAsync();

                // Now delete the user
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return result.Errors.FirstOrDefault()?.Description ?? "FailedToDeleteUser";

                return "Deleted";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }

}
