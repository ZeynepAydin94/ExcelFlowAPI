using System;
using ExcelFlow.Core.Dtos.UploadJob;

namespace ExcelFlow.Services.Interfaces;

public interface IAwsS3Service
{
    GeneratePreSignedUploadUrlResponseDto GeneratePreSignedUploadUrl();
    Task<Stream> DownloadFileAsync(string fileUrl, CancellationToken cancellationToken = default);
}