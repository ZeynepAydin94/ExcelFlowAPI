using System;
using ExcelFlow.Core.Dtos.ExcelTemplate;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;

namespace ExcelFlow.Services.Interfaces;

public interface IExcelTemplateService : IBaseService<ExcelTemplate, ExcelTemplateInsertDto, ExcelTemplateUpdateDto, ExcelTemplateResponseDto>
{
}
