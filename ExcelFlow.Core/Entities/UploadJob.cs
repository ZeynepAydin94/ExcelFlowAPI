using System;

namespace ExcelFlow.Core.Entities;

public class UploadJob : BaseEntity
{
    public string? FileName { get; set; }
    public string? FileUrl { get; set; }
    public string? Notes { get; set; }
    public int ExcelTemplateId { get; set; }
    public int StatusId { get; set; }
    public UploadStatus? Status { get; set; }

    public User? CreatedByUser { get; set; }
}