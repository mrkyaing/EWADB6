using CloudHRMS.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CloudHRMS.Services {
    public class AuthService : IAuthService {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IConfiguration config, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            this._config = config;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public async Task<(int, string)> AuthLogin(AuthViewModel authViewModel) {
            var loginnedUser = await _userManager.FindByNameAsync(authViewModel.UserName);

            if (loginnedUser == null) {
                return (401, "Invalid user!!");
            }
            var validatePassword = await _userManager.CheckPasswordAsync(loginnedUser, authViewModel.Password);

            if (!validatePassword) {
                return (401, "Invalid password!!");
            }
            //get the user roles
            var userRoles = await _userManager.GetRolesAsync(loginnedUser);

            //set the claim name and role to generate the token
            var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name,loginnedUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
            foreach (var userRole in userRoles) {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string generatedToke = GenerateToken(authClaims);
            return (200, generatedToke);
        }

        private string GenerateToken(IList<Claim> claims) {
            //reading the secret key from appsettings.json
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(3),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
