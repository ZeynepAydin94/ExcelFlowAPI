using System;
using ExcelFlow.Services.Interfaces;

namespace ExcelFlow.Services.Services;

public class ExcelProcessorService : IExcelProcessorService
{
    private readonly IS3Service _s3;
    private readonly IUploadJobRowService _uploadJobRowService;

    public ExcelProcessorService(IS3Service s3, IUploadJobRowService uploadJobRowService)
    {
        _s3 = s3;
        _uploadJobRowService = uploadJobRowService;
    }

    public async Task ProcessAsync(ExcelFileProcessMessage message)
    {
        var stream = await _s3.DownloadFileAsync(message.S3Url);

        var rows = ExcelParser.Parse(stream);

        var rowDtos = rows.Select((row, index) => new UploadJobRowInsertDto
        {
            UploadJobId = message.FileId,
            RowNumber = index + 1,
            Data = row,
            IsValid = true // örnek
        }).ToList();

        await _uploadJobRowService.BulkInsertAsync(rowDtos);
    }
}
