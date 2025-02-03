using IntegrationApplication.EF;
using IntegrationModels;

namespace IntegrationApplication.Repositories;

public class WeatherInfoRepository(IntegrationDbContext context) : GenericRepository<WeatherInfoEntity>(context)
{
    
}