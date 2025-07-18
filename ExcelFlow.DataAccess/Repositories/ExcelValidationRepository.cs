using System;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ExcelFlow.DataAccess.Repositories;

public class ExcelValidationRepository : IExcelValidationRepository
{
    private readonly AppDbContext _dbContext;

    public ExcelValidationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ExcelTemplateColumn>> GetColumnsWithValidationsAsync(int templateId, CancellationToken cancellationToken)
    {
        return await _dbContext.ExcelTemplateColumn
            .Where(x => x.ExcelTemplateId == templateId && x.IsActive && !x.IsDeleted)
            .Include(x => x.Validations) // Validations navigasyon property olmalı
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ValueExistsAsync(string table, string column, string value, CancellationToken cancellationToken)
    {
        var sql = $"SELECT COUNT(1) FROM [{table}] WHERE [{column}] = @p0";
        var result = await _dbContext.Database.ExecuteSqlRawAsync(sql, [value], cancellationToken);
        return result > 0;
    }

    public async Task<bool> ExecuteCustomSqlAsync(string sqlTemplate, string value, CancellationToken cancellationToken)
    {
        var sql = sqlTemplate.Replace("{value}", value.Replace("'", "''")); // SQL injection'a karşı
        var result = await _dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        return result > 0;
    }
}
