using System;

namespace ExcelFlow.Core.Entities;

public class ExcelTemplateColumn
{
    public int Id { get; set; }
    public int ExcelTemplateId { get; set; }

    public string ExcelColumnName { get; set; } = null!;
    public string TargetColumnName { get; set; } = null!;
    public string? DataType { get; set; } // örnek: "string", "decimal", "datetime"
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? CreatedByUserId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    public ExcelTemplate? Template { get; set; }

    public List<ExcelTemplateColumnValidation> Validations { get; set; } = new();
}