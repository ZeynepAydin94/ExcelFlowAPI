using System;
using ExcelFlow.Core.Dtos.UploadJob;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.Services.Services;

namespace ExcelFlow.Services.Interfaces;

public interface IUploadJobService : IBaseService<UploadJob, UploadJobInsertDto, UploadJobUpdateDto, UploadJobInsertResponseDto>
{
    public Task<UploadJobInsertResponseDto> CreateAndQueueAsync(UploadJobInsertDto dto);
}
