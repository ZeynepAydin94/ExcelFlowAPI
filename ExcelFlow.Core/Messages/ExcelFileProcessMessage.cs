using System;

namespace ExcelFlow.Core.Messages;

public class ExcelFileProcessMessage
{
    public int FileId { get; set; }
    public string S3Url { get; set; }
}
