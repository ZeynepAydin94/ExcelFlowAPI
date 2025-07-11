
using AutoMapper;
using ExcelFlow.Core.Dtos.UploadJobError;
using ExcelFlow.Core.Entities;

namespace ExcelFlow.Core.Mappings;

public class UploadJobErrorProfile : Profile
{
    public UploadJobErrorProfile()
    {
        // DTO -> Entity
        CreateMap<UploadJobErrorInsertDto, UploadJobError>();

        // Entity -> DTO
        CreateMap<UploadJobError, UploadJobErrorResponseDto>();
    }
}
