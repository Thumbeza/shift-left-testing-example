namespace We.Sell.Bread.Infrastructure.Data.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(IUnitOfWork<OrderDBContext> unitOfWork) : base(unitOfWork)
    {
    }

    public IEnumerable<Customer> GetCustomerByName(string name)
    {
        throw new NotImplementedException();
    }
}
