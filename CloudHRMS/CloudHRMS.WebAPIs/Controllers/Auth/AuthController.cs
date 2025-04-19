using Asp.Versioning;
using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.WebAPIs.Controllers.Auth {
    //specify to enable both v1 and v2
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/Auth")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        // 3 Injection type : Constructor Injection, Method Injection, Property Injection
        public AuthController(ILogger<AuthController> logger, IAuthService authService) {
            this._logger = logger;
            this._authService = authService;
        }

        [MapToApiVersion(1)]
        // POST: api/Auth/v1/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthViewModel authViewModel) {
            _logger.LogInformation("Request Body:", authViewModel);
            if (authViewModel == null) {
                return BadRequest("Login data is null");//400
            }
            // Perform login logic here
            var (status, generatedToken) = await _authService.AuthLogin(authViewModel);
            if (status != 200) {
                return Unauthorized(new { message = "Unauthorized", statusCode = "401" });//401
            }
            // For example, validate the user credentials and generate a JWT token
            return Ok(new { message = "Login successful with token", token = generatedToken, statusCode = "200" });//200
        }
    }
}
