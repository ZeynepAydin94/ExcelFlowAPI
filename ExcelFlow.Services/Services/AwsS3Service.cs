using System;
using ExcelFlow.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ExcelFlow.Services.Services;


public class AwsS3Service : IAwsS3Service
{
    private readonly IConfiguration _config;

    public AwsS3Service(IConfiguration config)
    {
        _config = config;
    }

    public string GeneratePreSignedUploadUrl(string fileName, string contentType, TimeSpan expiresIn)
    {
        // var bucketName = _config["AWS:BucketName"];
        // var region = RegionEndpoint.GetBySystemName(_config["AWS:Region"]);

        // var credentials = new BasicAWSCredentials(
        //     _config["AWS:AccessKey"],
        //     _config["AWS:SecretKey"]
        // );

        // var client = new AmazonS3Client(credentials, region);

        // var request = new GetPreSignedUrlRequest
        // {
        //     BucketName = bucketName,
        //     Key = fileName,
        //     Verb = HttpVerb.PUT,
        //     Expires = DateTime.UtcNow.Add(expiresIn),
        //     ContentType = contentType
        // };

        // return client.GetPreSignedURL(request);
        return "";
    }
}