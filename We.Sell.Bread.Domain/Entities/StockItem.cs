using System.Diagnostics.CodeAnalysis;
using We.Sell.Bread.Domain.Base;

namespace We.Sell.Bread.Domain.Entities;

[ExcludeFromCodeCoverage]
public class StockItem : BaseEntity
{
    public string Name {  get; set; }
    public int Quantity { get; set; } = 0;
    public decimal UnitPrice { get; set; }
}
