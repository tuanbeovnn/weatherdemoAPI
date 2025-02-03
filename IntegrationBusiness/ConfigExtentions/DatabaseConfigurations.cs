using IntegrationApplication;
using IntegrationApplication.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationBusiness.ConfigExtentions;

public static class DatabaseConfigurations
{
    public static IServiceCollection AddSqlOptions(this IServiceCollection services, IConfiguration configuration)
    {
        //Add DbContext
        services.AddDbContext<IntegrationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("WebSolutions"),
                cfg => cfg.EnableRetryOnFailure(3));
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}