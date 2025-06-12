using System;
using ExcelFlow.Core.Messages;

namespace ExcelFlow.Services.Interfaces;

public interface IRabbitMQPublisherService
{
    void PublishExcelProcessMessage(ExcelFileProcessMessage message);
}
