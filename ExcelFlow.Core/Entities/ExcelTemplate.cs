using System;

namespace ExcelFlow.Core.Entities;

public class ExcelTemplate
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string TargetTable { get; set; } = null!;
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? CreatedByUserId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    public List<ExcelTemplateColumn> Columns { get; set; } = new();
}