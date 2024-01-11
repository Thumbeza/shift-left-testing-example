using System.Data.Entity;
using System.Linq.Expressions;
using We.Sell.Bread.Domain.Exceptions;

namespace We.Sell.Bread.Infrastructure.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
{
    private DbSet<T> _entities;
    private bool _isDisposed;

    public GenericRepository(IUnitOfWork<OrderDBContext> unitOfWork) : this(unitOfWork.Context)
    {
    }

    public GenericRepository(OrderDBContext context)
    {
        _isDisposed = false;
        Context = context;
    }

    public OrderDBContext Context { get; set; }

    protected virtual DbSet<T> Entities
    {
        get { return _entities ??= Context.Set<T>(); }
    }

    public T Create(T entity)
    {
        Entities.Add(entity);

        return entity;
    }

    public T Update(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;

        return entity;
    }

    public bool Delete(T entity)
    {
        Entities.Remove(entity);

        return true;
    }

    public T Read(Expression<Func<T, bool>> expression)
    {
        try
        {
            return Entities.FirstOrDefault(expression);
        }
        catch (Exception exception)
        {
            throw new DatabaseException("The database could satisfy the user query.", exception);
        }
        
    }

    public List<T> ReadAll() => Entities.ToList();

    public void Dispose()
    {
        Context?.Dispose();

        _isDisposed = true;
    }
}
