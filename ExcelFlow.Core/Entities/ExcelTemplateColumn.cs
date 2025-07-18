using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelFlow.Core.Entities;

public class ExcelTemplateColumn : BaseEntity
{

    public int ExcelTemplateId { get; set; }

    public string ExcelColumnName { get; set; } = null!;
    public string TargetColumnName { get; set; } = null!;
    public string? DataType { get; set; } // örnek: "string", "decimal", "datetime"
    public string? Notes { get; set; }

    public ExcelTemplate? Template { get; set; }

    public List<ExcelTemplateColumnValidation> Validations { get; set; } = new();
}