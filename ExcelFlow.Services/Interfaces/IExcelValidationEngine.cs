using System;

namespace ExcelFlow.Services.Interfaces;

public interface IExcelValidationEngine
{
    Task<List<ValidationError>> ValidateAsync(
           List<Dictionary<string, string>> rows,
           int templateId,
           CancellationToken cancellationToken = default);
}
