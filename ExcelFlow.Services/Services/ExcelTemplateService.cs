using System;
using AutoMapper;
using ExcelFlow.Core.Dtos.ExcelTemplate;
using ExcelFlow.Core.Entities;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.Services.Interfaces;

namespace ExcelFlow.Services.Services;


public class ExcelTemplateService : BaseService<ExcelTemplate, ExcelTemplateInsertDto, ExcelTemplateUpdateDto, ExcelTemplateResponseDto>, IExcelTemplateService
{

    public ExcelTemplateService(IBaseRepository<ExcelTemplate> repository, IMapper mapper) : base(repository, mapper)
    {
    }

}

