using System;

namespace ExcelFlow.Core.Entities;

public class ExcelTemplateColumnValidation
{
    public int Id { get; set; }
    public int ExcelTemplateColumnId { get; set; }

    public string ValidationType { get; set; } = null!; // örn: Required, MaxLength, ExistsInTable, CustomSql
    public string? ValidationParameter { get; set; }    // örn: 50, "Musteri.MusteriKodu", SELECT COUNT...
    public string? ErrorMessage { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? CreatedByUserId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    public ExcelTemplateColumn Column { get; set; } = null!;
}