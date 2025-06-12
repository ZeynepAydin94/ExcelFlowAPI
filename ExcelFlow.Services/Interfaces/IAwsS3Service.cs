using System;

namespace ExcelFlow.Services.Interfaces;

public interface IAwsS3Service
{
    string GeneratePreSignedUploadUrl(string fileName, string contentType, TimeSpan expiresIn);
}