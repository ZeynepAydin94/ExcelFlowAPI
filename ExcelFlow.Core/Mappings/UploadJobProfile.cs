// Core/Mappings/MappingProfile.cs
using AutoMapper;
using ExcelFlow.Core.Dtos.UploadJob;
using ExcelFlow.Core.Entities;

public class UploadJobProfile : Profile
{
    public UploadJobProfile()
    {
        // DTO -> Entity
        CreateMap<UploadJobInsertDto, UploadJob>();

        // Entity -> DTO
        CreateMap<UploadJob, UploadJobInsertResponseDto>();
    }
}