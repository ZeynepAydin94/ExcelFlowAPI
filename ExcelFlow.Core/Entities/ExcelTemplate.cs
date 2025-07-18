using System;

namespace ExcelFlow.Core.Entities;

public class ExcelTemplate : BaseEntity
{
    public string Name { get; set; } = null!;
    public string TargetTable { get; set; } = null!;
    public string? Notes { get; set; }

    public List<ExcelTemplateColumn> Columns { get; set; } = new();
}