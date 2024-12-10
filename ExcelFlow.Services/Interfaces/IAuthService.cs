using System;

namespace ExcelFlow.Services.Interfaces;

public interface IAuthService
{
    string GenerateToken(string userId, string email, string[] roles);
    bool ValidatePassword(string password, string hashedPassword);
}
