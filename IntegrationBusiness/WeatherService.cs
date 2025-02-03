using IntegrationApplication;
using IntegrationDtos;
using IntegrationModels;

namespace IntegrationBusiness;

public class WeatherService
{
    private readonly WeatherApi _api;
    private readonly IUnitOfWork _uow;

    public WeatherService(WeatherApi api, IUnitOfWork uow)
    {
        _api = api;
        _uow = uow;
    }

    public async Task<bool> GetCurrentWeather(WeatherRequest request)
    {
        //1. Call API
        //2. Save data
        //3. Return response
        var response = await _api.GetCurrentWeather(request);
        if (response.Success)
        {
            _uow.WeatherInfo.Insert(new WeatherInfoEntity()
            {
                City = "demo"
            });
            return await _uow.SaveChangeAsync();
        }

        return false;
    }
}