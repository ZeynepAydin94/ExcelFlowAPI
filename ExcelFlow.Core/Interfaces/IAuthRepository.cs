using System;
using ExcelFlow.Core.Entities;

namespace ExcelFlow.Core.Interfaces;

public interface IAuthRepository : IBaseRepository<User>
{
    Task<User?> GetUserByUsernameAsync(string username);
}
