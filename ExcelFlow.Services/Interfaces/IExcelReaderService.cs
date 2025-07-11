using System;
using System.Data;

namespace ExcelFlow.Services.Interfaces;

public interface IExcelReaderService
{
    Task<List<Dictionary<string, string>>> ReadExcelAsync(Stream fileStream);
}
