using System.Linq.Expressions;

namespace We.Sell.Bread.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    T Create(T entity);
    T Update(T entity);
    bool Delete(T entity);
    T Read(Expression<Func<T, bool>> expression);
    List<T> ReadAll();
}
