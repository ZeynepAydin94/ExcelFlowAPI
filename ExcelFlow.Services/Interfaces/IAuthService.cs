using System;
using ExcelFlow.Core.Dtos.Login;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;

namespace ExcelFlow.Services.Interfaces;

public interface IAuthService : IBaseService<User, UserInsertDto, UserResponseDto>
{
    string GenerateToken(string userId, string email, string[] roles);
    bool ValidatePassword(string password, string hashedPassword);
    Task<User?> AuthenticateAsync(string email, string password); // Add this line
}
