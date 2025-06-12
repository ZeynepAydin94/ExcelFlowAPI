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

public class UploadJobService : BaseService<UploadJob, UploadJobInsertDto, UploadJobInsertResponseDto>, IUploadJobService
{
    private readonly IUploadJobRepository _IuploadJobRepository;
    private readonly IRabbitMQPublisherService _rabbitMQPublisher;
    public UploadJobService(IConfiguration configuration, IUploadJobRepository uploadJobRepository, IRabbitMQPublisherService rabbitMQPublisher, IMapper mapper)
        : base(uploadJobRepository, mapper)
    {
        _IuploadJobRepository = uploadJobRepository;
        _rabbitMQPublisher = rabbitMQPublisher;
    }
    public override Task PreInsertAsync(UploadJob entity)
    {
        entity.StatusId = (int)EUploadJobStatus.Uploaded;
        entity.FileName = Guid.NewGuid().ToString(); // Örnek dosya adı oluşturma
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
