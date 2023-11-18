namespace We.Sell.Bread.Infrastructure.Data.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(IUnitOfWork<OrderDBContext> unitOfWork) : base(unitOfWork)
    {
    }

    public IEnumerable<Order> GetOrderByPaymentStatus(bool isPaid)
    {
        throw new NotImplementedException();
    }
}
