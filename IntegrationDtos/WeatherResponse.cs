namespace IntegrationDtos;

public class WeatherResponse
{
    public string Id { get; set; }
    public string Country { get; set; }
    public List<Weather> Weather { get; set; }
}

public class Weather
{
    public string Description { get; set; }
    public string Id { get; set; }
    public string Icon { get; set; }
    public string Main { get; set; }
}