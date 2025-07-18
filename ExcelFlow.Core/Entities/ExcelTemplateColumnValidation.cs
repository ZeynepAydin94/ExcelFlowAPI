using System;

namespace ExcelFlow.Core.Entities;

public class ExcelTemplateColumnValidation : BaseEntity
{

    public int ExcelTemplateColumnId { get; set; }

    public string ValidationType { get; set; } = null!; // örn: Required, MaxLength, ExistsInTable, CustomSql
    public string? ValidationParameter { get; set; }    // örn: 50, "Musteri.MusteriKodu", SELECT COUNT...
    public string? ErrorMessage { get; set; }


    public ExcelTemplateColumn Column { get; set; } = null!;
}