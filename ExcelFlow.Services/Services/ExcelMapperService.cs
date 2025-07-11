using System;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.Services.Interfaces;

namespace ExcelFlow.Services.Services;

public class ExcelMapperService : IExcelMapperService
{
    private readonly IExcelTemplateRepository _templateRepo;

    public ExcelMapperService(IExcelTemplateRepository templateRepo)
    {
        _templateRepo = templateRepo;
    }

    public async Task InsertValidRowsAsync(List<Dictionary<string, string>> rows, int templateId, CancellationToken cancellationToken)
    {
        var template = await _templateRepo.GetTemplateWithColumnsAsync(templateId, cancellationToken);

        if (template == null)
            throw new Exception("Template not found");

        var columnMappings = template.Columns
            .Where(c => !string.IsNullOrEmpty(c.TargetColumnName))
            .ToList();

        foreach (var row in rows)
        {
            var insertData = new Dictionary<string, string>();

            foreach (var col in columnMappings)
            {
                if (row.TryGetValue(col.ExcelColumnName, out var value))
                {
                    insertData[col.TargetColumnName] = value;
                }
            }

            await _templateRepo.InsertRowAsync(template.TargetTable, insertData, cancellationToken);
        }
    }

}