using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace IntegrationBusiness.ConfigExtentions;

public static class LoggingConfigureExtensions
{
    public static ConfigureHostBuilder AddSerilog(this ConfigureHostBuilder builder)
    {
        builder.UseSerilog((hostingContext, services, loggerConfiguration) =>
        {
            loggerConfiguration.WriteTo.Console();
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
        }).ConfigureLogging(f =>
        {
            f.ClearProviders();
            f.AddSerilog();
        });
        return builder;
    }
}