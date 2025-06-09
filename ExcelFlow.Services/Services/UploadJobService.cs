using System;
using System.Security.Claims;
using AutoMapper;
using ExcelFlow.Core.Dtos.UploadJob;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ExcelFlow.Services.Services;

public class UploadJobService : BaseService<UploadJob, UploadJobInsertDto>, IUploadJobService
{
    private readonly IUploadJobRepository _IuploadJobRepository;
    public UploadJobService(IConfiguration configuration, IUploadJobRepository uploadJobRepository, IMapper mapper)
        : base(uploadJobRepository, mapper)
    {
        _IuploadJobRepository = uploadJobRepository;
    }

}
