using Microsoft.AspNetCore.Identity;

namespace CloudHRMS.Services {
    public class UserService : IUserService {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;


        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _signInManager = signInManager;
            this._roleManager = roleManager;
        }
        public async Task<string> CreateUserAsync(string userName, string password, string email) {
            var user = CreateUser();
            user.UserName = userName;
            user.Email = email;
            user.NormalizedUserName = userName;
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded) {
                var roleName = "EMPLOYEE";
                // Ensure the role exists before assigning it
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists) {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                await _userManager.AddToRoleAsync(user, roleName);//assign created user to "Employee" role
                return user.Id;
            }
            throw new Exception("error when user recrod is created with associated role");
        }

        public async Task<IList<string>> GetRolesByUserId(string userId) {
            var user = await _userManager.FindByIdAsync(userId); // to get current user
            if (user == null) {
                throw new InvalidOperationException($"User with ID '{userId}' not found.");
            }
            var roles = await _userManager.GetRolesAsync(user); // to get current user's roles
            return roles;
        }
        private IdentityUser CreateUser() {
            try {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
