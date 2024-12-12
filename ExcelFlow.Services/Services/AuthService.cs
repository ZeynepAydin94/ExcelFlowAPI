using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExcelFlow.Services.Services;

public class AuthService : BaseService<User>, IAuthService
{
    private readonly IConfiguration _configuration;

    private readonly IAuthRepository _authRepository;

    public AuthService(IConfiguration configuration, IAuthRepository authRepository) : base(authRepository)
    {
        _configuration = configuration;
        _authRepository = authRepository;

    }
    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        var user = await _authRepository.GetUserByUsernameAsync(email);
        var hash = Hashassword(password);
        if (user == null || !ValidatePassword(password, user.PasswordHash))
        {
            return null; // Authentication failed
        }
        return user;
    }
    // Token oluşturma metodu
    public string GenerateToken(string userId, string email, string[] roles)
    {
        var secretKey = _configuration["JwtSettings:SecretKey"];
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];

        var expirationMinutes = int.Parse(Encoding.UTF8.GetBytes(_configuration["JwtSettings:ExpirationMinutes"] ?? string.Empty));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        // Kullanıcı rolleri varsa ekle
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Şifre doğrulama metodu (örnek)
    public bool ValidatePassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
    public string Hashassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
