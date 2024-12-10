using System;

namespace ExcelFlow.Core.Entities;

public class BaseEntity
{
  public int RecordId { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }
  public bool IsActive { get; set; } = true;

  public bool IsDeleted { get; set; } = false;
}
