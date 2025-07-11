using System.Data;
using Microsoft.EntityFrameworkCore;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Enums;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.Services.Interfaces;


public class ExcelValidationEngine : IExcelValidationEngine
{
    private readonly IExcelValidationRepository _validationRepository;

    public ExcelValidationEngine(IExcelValidationRepository validationRepository)
    {
        _validationRepository = validationRepository;
    }

    public async Task<List<ValidationError>> ValidateAsync(
           List<Dictionary<string, string>> rows,
           int templateId,
           CancellationToken cancellationToken = default)
    {
        var errors = new List<ValidationError>();
        var columns = await _validationRepository.GetColumnsWithValidationsAsync(templateId, cancellationToken);

        for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
        {
            var row = rows[rowIndex];

            foreach (var column in columns)
            {
                var value = row.TryGetValue(column.ExcelColumnName, out var v) ? v : null;

                foreach (var rule in column.Validations.Where(v => v.IsActive && !v.IsDeleted))
                {
                    switch (rule.ValidationType)
                    {
                        case nameof(EValidationType.Required):
                            if (string.IsNullOrWhiteSpace(value))
                                errors.Add(new(rowIndex, column.ExcelColumnName, rule.ErrorMessage ?? "Required field"));
                            break;

                        case nameof(EValidationType.MaxLength):
                            if (int.TryParse(rule.ValidationParameter, out var max) && value?.Length > max)
                                errors.Add(new(rowIndex, column.ExcelColumnName, rule.ErrorMessage ?? $"Max {max} chars allowed"));
                            break;

                        case nameof(EValidationType.CustomSql):
                            var valid = await _validationRepository.ExecuteCustomSqlAsync(rule.ValidationParameter!, value!, cancellationToken);
                            if (!valid)
                                errors.Add(new(rowIndex, column.ExcelColumnName, rule.ErrorMessage ?? "Custom SQL validation failed"));
                            break;
                    }
                }
            }
        }

        return errors;
    }
}
public record ValidationError(int RowIndex, string ColumnName, string ErrorMessage);