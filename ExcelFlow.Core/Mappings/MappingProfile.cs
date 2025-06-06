// Core/Mappings/MappingProfile.cs
using AutoMapper;
using ExcelFlow.Core.Dtos.UploadJob;
using ExcelFlow.Core.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // DTO -> Entity
        CreateMap<CreateUploadJobDto, UploadJob>();

        // Entity -> DTO
        CreateMap<UploadJob, CreateUploadJobDto>();
    }
}