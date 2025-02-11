using IntegrationApplication.EF;
using IntegrationApplication.Repositories;

namespace IntegrationApplication;

public interface IUnitOfWork
{
    WeatherInfoRepository WeatherInfo { get; }
    PostRepository PostRepository { get; }

    Task<bool> SaveChangeAsync();
    bool SaveChange();

    bool HasChanges();
}

public class UnitOfWork(IntegrationDbContext context) : IUnitOfWork
{
    public WeatherInfoRepository WeatherInfo => new WeatherInfoRepository(context);
    public PostRepository PostRepository => new PostRepository(context);

    public async Task<bool> SaveChangeAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool SaveChange()
    {
        return context.SaveChanges() > 0;
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
}