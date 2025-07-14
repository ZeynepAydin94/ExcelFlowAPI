using System;

namespace ExcelFlow.Core.Messages;

public class ExcelFileProcessMessage
{
    public int FileId { get; set; }
    public required string S3Url { get; set; }
}
