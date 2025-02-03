using System.Net;
using IntegrationDtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Registry;

namespace IntegrationBusiness;

public class WeatherApi
{
    private readonly WeatherOptions _options;
    private readonly ResiliencePipelineProvider<string> _pipelineProvider;

    public WeatherApi(IOptions<WeatherOptions> options, ResiliencePipelineProvider<string> pipelineProvider)
    {
        _options = options.Value;
        _pipelineProvider = pipelineProvider;
    }

    public async Task<Response<WeatherResponse>> GetCurrentWeather(WeatherRequest weatherRequest,
        CancellationToken cancellationToken = default)
    {
        ResiliencePipeline pipeline =
            _pipelineProvider.GetPipeline("retrie5Times");

        using HttpClient client = new()
        {
            BaseAddress = new Uri(_options.BaseAddress),
        };

        string requestUri = $"appId={_options.AppId}&units={weatherRequest.Units}&q={weatherRequest.Query}";


        var response = await pipeline.ExecuteAsync(async token =>

            {
                var res = await client.GetAsync("/data/2.5/weather?" + requestUri, token);

                if (res.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException("Not found");
                }

                return res;
            },
            cancellationToken);

        //var response = await client.GetAsync("/data/2.5/weather?" + requestUri);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            try
            {
                var info = JsonConvert.DeserializeObject<WeatherResponse>(data);
                return new SuccessResponse<WeatherResponse>("Weather data retrieved successfully")
                {
                    Data = info
                };
            }
            catch (Exception)
            {
                return new ErrorResponse<WeatherResponse>("Failed to parse weather data.");
            }
        }

        return new ErrorResponse<WeatherResponse>("Failed to fetch weather data from API.");
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}