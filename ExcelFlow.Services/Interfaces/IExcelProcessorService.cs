using System;
using ExcelFlow.Core.Messages;

namespace ExcelFlow.Services.Interfaces;

public interface IExcelProcessorService
{
    Task ProcessAsync(ExcelFileProcessMessage message);
}
