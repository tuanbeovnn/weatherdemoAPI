using Microsoft.Extensions.Configuration;
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
        return services;
    }
}