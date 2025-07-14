using System;

namespace ExcelFlow.Core.Dtos.UploadJob;

public class GeneratePreSignedUploadUrlResponseDto
{
    public required string Url { get; set; }
    public required string FileName { get; set; }
}
