using IntegrationDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationBusiness.ConfigExtentions;

public static class OptionsConfigurations
{
    public static IServiceCollection AddAllOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WeatherOptions>(configuration.GetSection(WeatherOptions.Name));
        return services;
    }
}