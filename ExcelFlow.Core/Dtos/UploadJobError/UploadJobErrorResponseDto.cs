using System;

namespace ExcelFlow.Core.Dtos.UploadJobError;

public class UploadJobErrorResponseDto
{
    public int RowIndex { get; set; }
    public string ColumnName { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;
}