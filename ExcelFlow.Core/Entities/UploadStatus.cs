using System;


namespace ExcelFlow.Core.Entities;

public class UploadStatus : BaseEntity
{
    public string Name { get; set; }

    public User CreatedByUser { get; set; }
}