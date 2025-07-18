using System;
using AutoMapper;
using ExcelFlow.Core.Dtos.UploadJobError;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.Services.Interfaces;
namespace ExcelFlow.Services.Services;

public class UploadJobErrorService : BaseService<UploadJobError, UploadJobErrorInsertDto, UploadJobErrorUpdateDto, UploadJobErrorResponseDto>, IUploadJobErrorService
{

    public UploadJobErrorService(IBaseRepository<UploadJobError> repository, IMapper mapper) : base(repository, mapper)
    {
    }

}
