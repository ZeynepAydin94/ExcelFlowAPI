using ExcelFlow.Core.Dtos.Request;
using ExcelFlow.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcelFlow.Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Endpoint to authenticate a user and generate a JWT token.
        /// </summary>
        /// <param name="request">Login request containing username and password</param>
        /// <returns>A JWT token if authentication is successful</returns>
        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] LoginRequest request)
        {
            // Validate the request
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            // Authenticate the user (you need to implement this logic in AuthService)
            var user = await _authService.AuthenticateAsync(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Generate token
            var token = _authService.GenerateToken(user.RecordId.ToString(), user.Email, new string[0]);
            return Ok(new { Token = token });
        }
        [HttpGet("hello")]
        public IActionResult SayHello()
        {
            return Ok("Hello, World!");
        }
    }
}
