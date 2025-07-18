using System;
using Amazon.S3;
using Amazon.S3.Model;
using ExcelFlow.Core.Dtos.UploadJob;
using ExcelFlow.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ExcelFlow.Services.Services;


public class AwsS3Service : IAwsS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string? _bucketName;
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

    public GeneratePreSignedUploadUrlResponseDto GeneratePreSignedUploadUrl()
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
        var response = _s3Client.GetPreSignedURL(request);
        return new GeneratePreSignedUploadUrlResponseDto
        {
            Url = response,
            FileName = request.Key
        };
    }

    public async Task<Stream> DownloadFileAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        // https://bucket.s3.region.amazonaws.com/key -> sadece key kısmını ayıkla
        var uri = new Uri(fileUrl);
        var bucket = _bucketName;
        var key = uri.AbsolutePath.TrimStart('/');

        var response = await _s3Client.GetObjectAsync(bucket, key, cancellationToken);
        return response.ResponseStream;
    }
}