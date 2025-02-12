using System.Linq.Expressions;
using IntegrationApplication.EF;
using IntegrationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IntegrationApplication.Repositories;

public interface IGenericRepository<T> where T : ModelBaseEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetByIdAsync(object id);

    void Insert(T obj);

    void Update(T obj);
    void Delete(object id);

    bool Save();
    Task<bool> SaveAsync();
    void Update<TProperty>(T entity, Expression<Func<T, TProperty>> property, TProperty newValue);
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


    public async Task<IEnumerable<T>> GetAll()
    {
        return await _table.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(object id)
    {
        return await _table.FindAsync(id);
    }

    public void Insert(T obj)
    {
        _table.Add(obj);
    }

    public void Update(T obj)
    {
        _table.Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
    }

    public void Delete(object id)
    {
        var existing = _table.Find(id);
        if (existing != null)
        {
            _table.Remove(existing);
        }
    }

    public void Update<TProperty>(T entity, Expression<Func<T, TProperty>> property, TProperty newValue)
    {
        var entry = _context.Entry(entity);
        entry.Property(property).CurrentValue = newValue;
        entry.Property(property).IsModified = true;
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