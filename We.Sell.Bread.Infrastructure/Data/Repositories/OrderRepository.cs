namespace We.Sell.Bread.Infrastructure.Data.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(IUnitOfWork<OrderDBContext> unitOfWork) : base(unitOfWork)
    {
    }

    public Order GetOrderById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetOrderByPaymentStatus(bool isPaid)
    {
        throw new NotImplementedException();
    }
}
