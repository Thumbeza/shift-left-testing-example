using System.Diagnostics.CodeAnalysis;

namespace Shift.Left.Testing.Poc.Models;

[ExcludeFromCodeCoverage]
public class FunctionSummary
{
    public int FirstNumber { get; set; }
    public int SecondNumber { get; set; }
    public int Result { get; set; }
    public string? Type { get; set; }
    public string? Error { get; set; }
}
