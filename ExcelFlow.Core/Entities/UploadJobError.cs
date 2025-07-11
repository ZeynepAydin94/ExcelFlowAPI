using System;

namespace ExcelFlow.Core.Entities;

public class UploadJobError
{
    public int RecordId { get; set; }
    public int UploadJobId { get; set; }
    public int RowIndex { get; set; }
    public string ColumnName { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? CreatedByUserId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    public UploadJob? UploadJob { get; set; }
}
