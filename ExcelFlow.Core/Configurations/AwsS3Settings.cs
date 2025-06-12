using System;

namespace ExcelFlow.Core.Configurations;

public class AwsS3Settings
{
    public string BucketName { get; set; }
    public string Region { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
}
