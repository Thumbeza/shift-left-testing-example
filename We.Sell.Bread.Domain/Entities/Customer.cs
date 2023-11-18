using System.Diagnostics.CodeAnalysis;
using We.Sell.Bread.Domain.Base;

namespace We.Sell.Bread.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Customer : BaseEntity
{
    public string CustomerName { get; set; }
    public string ContactNo {  get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public string PhysicalAddress { get; set; }
}
