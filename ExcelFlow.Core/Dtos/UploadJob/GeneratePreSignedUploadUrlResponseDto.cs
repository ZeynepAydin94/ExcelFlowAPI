using System;

namespace ExcelFlow.Core.Dtos.UploadJob;

public class GeneratePreSignedUploadUrlResponseDto
{
    public string Url { get; set; }
    public string FileName { get; set; }
}
