using System;

namespace ExcelFlow.Services.Interfaces;

public interface IExcelMapperService
{
    Task InsertValidRowsAsync(List<Dictionary<string, string>> rows, int templateId, CancellationToken cancellationToken);
}