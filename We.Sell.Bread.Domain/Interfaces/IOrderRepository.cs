using We.Sell.Bread.Domain.Entities;

namespace We.Sell.Bread.Domain.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    IEnumerable<Order> GetOrderByPaymentStatus(bool isPaid);
    Order GetOrderById(Guid id);
}
