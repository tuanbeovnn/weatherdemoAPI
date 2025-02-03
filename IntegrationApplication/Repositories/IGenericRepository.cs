using System.Linq.Expressions;
using IntegrationApplication.EF;
using IntegrationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IntegrationApplication.Repositories;

public interface IGenericRepository<T> where T : ModelBaseEntity
{
    IEnumerable<T> GetAll();
    T? GetById(object id);

    void Insert(T obj);

    bool Save();
    Task<bool> SaveAsync();
}

public abstract class GenericRepository<T> : IGenericRepository<T> where T : ModelBaseEntity
{
    protected readonly IntegrationDbContext _context;
    private readonly DbSet<T> _table;

    public GenericRepository(IntegrationDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }


    public IEnumerable<T> GetAll()
    {
        return _table.ToList();
    }

    public T? GetById(object id)
    {
        return _table.Find(id);
    }

    public void Insert(T obj)
    {
        _table.Add(obj);
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }

    public async Task<bool> SaveAsync()
    {
        int rows = await _context.SaveChangesAsync();
        return rows > 0;
    }
}

public static class RepositoryExtensions
{
    public static EntityEntry<T> SetProperty<T, TProperty>(this EntityEntry<T> entry,
        Expression<Func<T, TProperty>> property,
        TProperty newValue) where T : ModelBaseEntity
    {
        entry.Property(property).CurrentValue = newValue;
        entry.Property(property).IsModified = true;
        return entry;
    }
}