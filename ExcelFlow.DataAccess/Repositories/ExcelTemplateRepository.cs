using System;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ExcelFlow.DataAccess.Repositories;


public class ExcelTemplateRepository : BaseRepository<ExcelTemplate>, IExcelTemplateRepository
{

    public ExcelTemplateRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<ExcelTemplate?> GetTemplateWithColumnsAsync(int templateId, CancellationToken cancellationToken)
    {
        return await _context.ExcelTemplate
            .Include(t => t.Columns)
            .FirstOrDefaultAsync(t => t.RecordId == templateId, cancellationToken);
    }

    public async Task InsertRowAsync(string tableName, Dictionary<string, string> columnValues, CancellationToken cancellationToken)
    {
        var columns = columnValues.Keys;
        var values = columnValues.Values.Select(v => $"'{v.Replace("'", "''")}'");

        var sql = $"INSERT INTO {tableName} ({string.Join(",", columns)}) VALUES ({string.Join(",", values)})";
        await _context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }
}