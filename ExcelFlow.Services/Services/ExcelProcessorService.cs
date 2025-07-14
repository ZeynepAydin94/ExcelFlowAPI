using System.Data;
using AutoMapper;
using ExcelFlow.Core.Dtos.UploadJob;
using ExcelFlow.Core.Dtos.UploadJobError;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Enums;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.Core.Messages;
using ExcelFlow.Services.Interfaces;

public class ExcelProcessorService : IExcelProcessorService
{
    private readonly IUploadJobService _uploadJobService;
    private readonly IUploadJobErrorService _uploadJobErrorService;
    private readonly IExcelValidationEngine _validationEngine;
    private readonly IAwsS3Service _s3Service;
    private readonly IExcelReaderService _excelReader;
    private readonly IMapper _mapper;
    private readonly IExcelMapperService _mapperService;
    public ExcelProcessorService(
        IUploadJobService uploadJobService,
        IExcelValidationEngine validationEngine,
        IAwsS3Service s3Service,
        IExcelReaderService excelReader, IUploadJobErrorService uploadJobErrorService, IMapper mapper, IExcelMapperService mapperService)
    {
        _uploadJobService = uploadJobService;
        _validationEngine = validationEngine;
        _s3Service = s3Service;
        _excelReader = excelReader;
        _uploadJobErrorService = uploadJobErrorService;
        _mapper = mapper;
        _mapperService = mapperService;
    }

    public async Task ProcessAsync(int uploadJobId, CancellationToken cancellationToken = default)
    {
        UploadJobUpdateDto dto = new UploadJobUpdateDto();
        UploadJob job = await _uploadJobService.GetByIdAsync(uploadJobId);
        if (job == null || string.IsNullOrEmpty(job.FileUrl))
            throw new Exception("UploadJob not found or file URL is empty");

        var stream = await _s3Service.DownloadFileAsync(job.FileUrl, cancellationToken);
        var rows = await _excelReader.ReadExcelAsync(stream);

        var errors = await _validationEngine.ValidateAsync(rows, job.ExcelTemplateId, cancellationToken);
        if (errors.Any())
        {
            var errorDtos = errors.Select(e => new UploadJobErrorInsertDto
            {
                RowIndex = e.RowIndex,
                ColumnName = e.ColumnName,
                ErrorMessage = e.ErrorMessage,
                UploadJobId = uploadJobId
            }).ToList();
            foreach (var error in errorDtos)
            {
                await _uploadJobErrorService.CreateAsync(error);
            }

            // Job status güncelle
            job.StatusId = (int)EUploadJobStatus.Failed;

            dto = _mapper.Map<UploadJobUpdateDto>(job);
            dto.StatusId = (int)EUploadJobStatus.Failed;
            await _uploadJobService.UpdateAsync(uploadJobId, dto);

            return;
        }

        // ⬇️ Eğer geçerli veriyse: Insert et
        await _mapperService.InsertValidRowsAsync(rows, job.ExcelTemplateId, cancellationToken);

        dto = _mapper.Map<UploadJobUpdateDto>(job);
        dto.StatusId = (int)EUploadJobStatus.Completed;
        await _uploadJobService.UpdateAsync(uploadJobId, dto);

    }
}