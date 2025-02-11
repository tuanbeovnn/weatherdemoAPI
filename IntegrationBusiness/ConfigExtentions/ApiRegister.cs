using AutoMapper;
using IntegrationBusiness.Mappers;
using IntegrationBusiness.Services;
using IntegrationBusiness.Services.Impl;
using IntegrationBusiness.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationBusiness.ConfigExtentions;

public static class ApiRegister
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddScoped<WeatherApi>();
        return services;
    }

    public static IServiceCollection AddAllServices(this IServiceCollection services)
    {
        services.AddScoped<WeatherService>();
        services.AddScoped<IPostService, PostServiceImpl>();
        return services;
    }

    public static IServiceCollection AddMapperAndValidation(this IServiceCollection services)
    {
        var mapperConfiguration = new MapperConfiguration(
            config => { config.AddProfile<PostMappingProfile>(); });

        IMapper mapper = mapperConfiguration.CreateMapper();
        services.AddSingleton(mapper);
        services.AddSingleton<IValidationFactory, ValidationFactory>();
        return services;
    }
}