using System;
using ExcelFlow.Core.Entities;

namespace ExcelFlow.Core.Interfaces;

public interface IExcelValidationRepository
{
    Task<List<ExcelTemplateColumn>> GetColumnsWithValidationsAsync(int templateId, CancellationToken cancellationToken);
    Task<bool> ValueExistsAsync(string table, string column, string value, CancellationToken cancellationToken);
    Task<bool> ExecuteCustomSqlAsync(string sql, string value, CancellationToken cancellationToken);
}