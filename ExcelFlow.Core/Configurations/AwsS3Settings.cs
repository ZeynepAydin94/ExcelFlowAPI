using System;

namespace ExcelFlow.Core.Configurations;

public class AwsS3Settings
{
    public required string BucketName { get; set; }
    public required string Region { get; set; }
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
}
