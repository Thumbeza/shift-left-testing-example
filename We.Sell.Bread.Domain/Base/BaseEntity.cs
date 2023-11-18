using System.Diagnostics.CodeAnalysis;

namespace We.Sell.Bread.Domain.Base;

[ExcludeFromCodeCoverage]
public class BaseEntity
{
    public Guid Id { get; set; }
}
