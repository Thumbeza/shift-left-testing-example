using System.Diagnostics.CodeAnalysis;

namespace We.Sell.Bread.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Email
{
    public string Subject { get; set; }
    public string Body { get; set; }
}
