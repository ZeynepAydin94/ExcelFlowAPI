using System;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ExcelFlow.DataAccess.Repositories;

public class AuthRepository : BaseRepository<User>, IAuthRepository
{
    public AuthRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<User?> GetUserByUsernameAsync(string email)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
    }
}