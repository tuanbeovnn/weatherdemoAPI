using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace IntegrationBusiness.ConfigExtentions;

public static class HttpConfigurations
{
    public static IServiceCollection AddHttpResiliencePipeline(this IServiceCollection services)
    {
        services.AddResiliencePipeline("retrie5Times", builder =>
        {
            builder.AddRetry(new RetryStrategyOptions
            {
                Delay = TimeSpan.FromSeconds(1),
                MaxRetryAttempts = 5,
                ShouldHandle = new PredicateBuilder()
                    .Handle<NotFoundException>()
                    .Handle<FileNotFoundException>(),
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = true,
                OnRetry = retryArguments =>
                {
                    Console.WriteLine($"Screenshot failed with exception {retryArguments.Outcome.Exception.Message}. " +
                                      $"Waiting {retryArguments.Duration} before next retry. Retry attempt {retryArguments.AttemptNumber}");
                    return ValueTask.CompletedTask;
                }
            }).AddTimeout(TimeSpan.FromMinutes(0.2));
        });
        return services;
    }
}