using System;

namespace ExcelFlow.Core.Dtos.UploadJobError;

public class UploadJobErrorInsertDto
{
    public int RowIndex { get; set; }
    public string ColumnName { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;
    public int UploadJobId { get; set; }
}