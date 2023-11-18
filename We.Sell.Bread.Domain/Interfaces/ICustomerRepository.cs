using We.Sell.Bread.Domain.Entities;

namespace We.Sell.Bread.Domain.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        IEnumerable<Customer> GetCustomerByName(string name);
    }
}
