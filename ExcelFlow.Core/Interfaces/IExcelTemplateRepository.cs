using System;
using ExcelFlow.Core.Entities;

namespace ExcelFlow.Core.Interfaces;

public interface IExcelTemplateRepository : IBaseRepository<ExcelTemplate>
{
    Task<ExcelTemplate?> GetTemplateWithColumnsAsync(int templateId, CancellationToken cancellationToken);
    Task InsertRowAsync(string tableName, Dictionary<string, string> columnValues, CancellationToken cancellationToken);
}
