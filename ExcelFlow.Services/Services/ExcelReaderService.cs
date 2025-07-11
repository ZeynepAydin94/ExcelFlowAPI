using System;
using ClosedXML.Excel;
using ExcelFlow.Services.Interfaces;

namespace ExcelFlow.Services.Services;

public class ExcelReaderService : IExcelReaderService
{
    public Task<List<Dictionary<string, string>>> ReadExcelAsync(Stream fileStream)
    {
        var result = new List<Dictionary<string, string>>();

        using var workbook = new XLWorkbook(fileStream);
        var worksheet = workbook.Worksheets.First();
        var headerRow = worksheet.Row(1);

        var headers = headerRow.CellsUsed().Select(c => c.Value.ToString()).ToList();

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            var rowDict = new Dictionary<string, string>();
            for (int i = 0; i < headers.Count; i++)
            {
                var key = headers[i];
                var value = row.Cell(i + 1).GetValue<string>();
                rowDict[key] = value;
            }
            result.Add(rowDict);
        }

        return Task.FromResult(result);
    }
}
