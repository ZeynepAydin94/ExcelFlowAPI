using ExcelFlow.Core.Dtos.Request;
using ExcelFlow.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcelFlow.API.Controllers
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Kullanıcıyı doğrula (örneğin, veritabanından kontrol)
            // Örnek olarak statik bir kullanıcı kontrolü
            if (request.Email == "user@example.com" && request.Password == "password")
            {
                var token = _authService.GenerateToken("1", request.Email, new[] { "User" });
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid email or password");
        }
        [HttpGet("hello")]
        public IActionResult SayHello()
        {
            return Ok("Hello, World!");
        }
    }
}
