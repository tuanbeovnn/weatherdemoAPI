using IntegrationBusiness;
using IntegrationDtos;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherService _weather;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherService weather)
    {
        _logger = logger;
        _weather = weather;
    }


    [HttpGet(Name = "GetCurrent")]
    public Task<bool> GetAll([FromQuery] WeatherRequest request)
    {
        return _weather.GetCurrentWeather(request);
    }
}