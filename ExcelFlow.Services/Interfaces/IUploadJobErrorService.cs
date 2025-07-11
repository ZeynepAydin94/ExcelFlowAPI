using System;
using ExcelFlow.Core.Dtos.UploadJobError;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;

namespace ExcelFlow.Services.Interfaces;

public interface IUploadJobErrorService : IBaseService<UploadJobError, UploadJobErrorInsertDto, UploadJobErrorUpdateDto, UploadJobErrorResponseDto>
{
}