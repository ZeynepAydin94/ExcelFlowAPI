using System;
using System.Security.Claims;
using ExcelFlow.Core.Dtos.UploadJob;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ExcelFlow.Services.Services;

public class UploadJobService : BaseService<UploadJob>, IUploadJobService
{
    private readonly IUploadJobRepository _IuploadJobRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UploadJobService(IConfiguration configuration, IUploadJobRepository uploadJobRepository) : base(uploadJobRepository)
    {
        _IuploadJobRepository = uploadJobRepository;
    }
    public async Task<UploadJob> CreateAsync(CreateUploadJobDto dto)
    {
        var userIdStr = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = userIdStr != null ? int.Parse(userIdStr) : 0;

        var uploadJob = new UploadJob
        {
            FileUrl = dto.FileUrl,
            FileName = dto.FileName,
            StatusId = 1, // Başlangıç statusü
            CreatedByUserId = userId
        };

        return await _uploadJobRepository.AddAsync(uploadJob);
    }
}
