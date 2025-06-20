using System;

namespace ExcelFlow.Services.Interfaces;

public interface IAwsS3Service
{
    string GeneratePreSignedUploadUrl();
}