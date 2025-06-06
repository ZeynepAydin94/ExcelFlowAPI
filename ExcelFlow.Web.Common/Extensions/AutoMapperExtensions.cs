using System;
using Microsoft.Extensions.DependencyInjection;
namespace ExcelFlow.Web.Common.Extensions;


public static class AutoMapperExtensions
{
    public static IServiceCollection AddAutoMapperMappings(this IServiceCollection services)
    {
        return services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}