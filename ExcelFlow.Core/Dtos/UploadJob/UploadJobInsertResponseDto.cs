using System;

namespace ExcelFlow.Core.Dtos.UploadJob;

public class UploadJobInsertResponseDto
{
    public int RecordId { get; set; }
    public required string FileUrl { get; set; }
}
