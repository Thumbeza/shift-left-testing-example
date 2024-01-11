using System.Diagnostics.CodeAnalysis;
using We.Sell.Bread.Domain.Base;

namespace We.Sell.Bread.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Order : BaseEntity
{
    public Order()
    {
        StockItems = new HashSet<StockItem>();
    }

    public Customer Customer { get; set; }
    public IEnumerable<StockItem> StockItems { get; set; }
    public decimal OrderTotal { get; set; }
    public bool IsPaid { get; set; } = false;
}
