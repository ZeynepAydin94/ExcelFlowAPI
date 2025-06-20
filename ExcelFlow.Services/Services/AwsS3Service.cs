using System;
using Amazon.S3;
using Amazon.S3.Model;
using ExcelFlow.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ExcelFlow.Services.Services;


public class AwsS3Service : IAwsS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly int _preSignedUrlExpirationMinutes;
    public AwsS3Service(IConfiguration config)
    {
        var accessKey = config["AWS:AccessKey"];
        var secretKey = config["AWS:SecretKey"];
        var region = config["AWS:Region"];
        _bucketName = config["AWS:BucketName"];
        _preSignedUrlExpirationMinutes = int.Parse(config["AWS:PreSignedUrlExpirationMinutes"] ?? "10");
        var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);
        _s3Client = new AmazonS3Client(awsCredentials, Amazon.RegionEndpoint.GetBySystemName(region));
    }

    public string GeneratePreSignedUploadUrl()
    {
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = Guid.NewGuid().ToString(), // Generate a unique file name
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddMinutes(_preSignedUrlExpirationMinutes),
            ContentType = contentType
        };

        return _s3Client.GetPreSignedURL(request);
    }
}