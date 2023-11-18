using System.Data.Entity;

namespace We.Sell.Bread.Domain.Interfaces;

public interface IUnitOfWork<out TContext> where TContext : DbContext, new()
{
    TContext Context { get; }
    void CreateTransaction();
    void Commit();
    void Rollback();
    Task<int> SaveChangesAsync();
}
