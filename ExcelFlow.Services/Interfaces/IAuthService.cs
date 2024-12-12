using System;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;

namespace ExcelFlow.Services.Interfaces;

public interface IAuthService : IBaseService<User>
{
    string GenerateToken(string userId, string email, string[] roles);
    bool ValidatePassword(string password, string hashedPassword);
    Task<User?> AuthenticateAsync(string email, string password); // Add this line
}
