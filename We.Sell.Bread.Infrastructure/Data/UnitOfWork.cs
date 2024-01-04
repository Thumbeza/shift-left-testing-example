using System.Data.Entity;
using We.Sell.Bread.Infrastructure.Exceptions;

namespace We.Sell.Bread.Infrastructure.Data;

public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where TContext : DbContext, new()
{
    private bool _disposed;
    private DbContextTransaction _dbTransaction;

    public UnitOfWork()
    {
        Context = new TContext();
    }

    public TContext Context { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void CreateTransaction()
    {
        _dbTransaction = Context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _dbTransaction.Commit();
    }

    public void Rollback()
    {
        _dbTransaction.Rollback();
        _dbTransaction.Dispose();
    }

    //Save changes to the database
    public Task<int> SaveChangesAsync()
    {
        try
        {
            return Context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new DatabaseException("Could not save changes to the Database", exception.InnerException);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                Context.Dispose();
        }

        _disposed = true;
    }
}
