namespace We.Sell.Bread.Infrastructure.Data.Repositories;

public class StockItemRepository : GenericRepository<StockItem>
{
    public StockItemRepository(IUnitOfWork<OrderDBContext> unitOfWork) : base(unitOfWork)
    {
    }
}
