using System;
using System.Security.Claims;
using AutoMapper;
using ExcelFlow.Core.Dtos.UploadJob;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Enums;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.Core.Messages;
using ExcelFlow.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ExcelFlow.Services.Services;

public class UploadJobService : BaseService<UploadJob, UploadJobInsertDto, UploadJobUpdateDto, UploadJobInsertResponseDto>, IUploadJobService
{
    private readonly IUploadJobRepository _IuploadJobRepository;
    private readonly IRabbitMQPublisherService _rabbitMQPublisher;
    private readonly IConfiguration config;
    public UploadJobService(IConfiguration configuration, IUploadJobRepository uploadJobRepository, IRabbitMQPublisherService rabbitMQPublisher, IMapper mapper)
        : base(uploadJobRepository, mapper)
    {
        _IuploadJobRepository = uploadJobRepository;
        _rabbitMQPublisher = rabbitMQPublisher;
        config = configuration;
    }
    public override Task PreInsertAsync(UploadJob entity)
    {

        entity.StatusId = (int)EUploadJobStatus.Uploaded;
        entity.FileUrl = $"https://{config["AWS:BucketName"]}.s3.{config["AWS:Region"]}.amazonaws.com/{entity.FileName}";
        return base.PreInsertAsync(entity);
    }



    public async Task<UploadJobInsertResponseDto> CreateAndQueueAsync(UploadJobInsertDto dto)
    {
        var uploadJob = await CreateAsync(dto);

        var message = new ExcelFileProcessMessage
        {
            FileId = uploadJob.RecordId,
            S3Url = uploadJob.FileUrl
        };

        _rabbitMQPublisher.PublishExcelProcessMessage(message);

        return uploadJob;
    }
}
