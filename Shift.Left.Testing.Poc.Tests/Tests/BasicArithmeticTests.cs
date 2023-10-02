using Moq;
using Shift.Left.Testing.Poc.Controllers;
using Shift.Left.Testing.Poc.Models;

namespace Shift.Left.Testing.Poc.Tests.Tests;

public class BasicArithmeticTests
{
    private readonly ILogger<BasicArithmeticController> _testLogger;
    private readonly BasicArithmeticController _basicArithmetic;
    public BasicArithmeticTests()
    {
        var mock = new Mock<ILogger<BasicArithmeticController>>();

        _testLogger = mock.Object;
        _basicArithmetic = new BasicArithmeticController(_testLogger);
    }

    [Fact]
    public void FunctionSumarryShouldBeOfFunctionSummaryType()
    {
        var functionSummary = _basicArithmetic.Add(2, 53);

        functionSummary.Should().BeOfType<FunctionSummary>();
    }

    [Fact]
    public void FunctionTypeIsAdditionWhenAdding()
    {
        var functionSummary = _basicArithmetic.Add(2, 53);

        functionSummary.Type.Should().Be("Addition");
    }

    [Fact]
    public void AddingPositiveIntegers()
    {
        var functionSummary = _basicArithmetic.Add(2, 53);

        functionSummary.Result.Should().Be(55);
    }

    [Fact]
    public void AddingPositiveAndNegativeIntegers()
    {
        var functionSummary = _basicArithmetic.Add(2, -53);

        functionSummary.Result.Should().Be(-51);
    }

    [Fact]
    public void AddingNegativeIntegers()
    {
        var functionSummary = _basicArithmetic.Add(-2, -53);

        functionSummary.Result.Should().Be(-55);
    }

    [Fact]
    public void FunctionTypeIsSubtractionWhenSubtracting()
    {
        var functionSummary = _basicArithmetic.Subtract(2, 53);

        functionSummary.Type.Should().Be("Subtraction");
    }

    [Fact]
    public void SubtractingPositiveIntegers()
    {
        var functionSummary = _basicArithmetic.Subtract(45, 3);

        functionSummary.Result.Should().Be(42);
    }

    [Fact]
    public void SubtractingBigPositiveFromSmallPositiveIntegers()
    {
        var functionSummary = _basicArithmetic.Subtract(3, 45);

        functionSummary.Result.Should().Be(-42);
    }

    [Fact]
    public void SubtractingBigNegativeFromSmallPositiveIntegers()
    {
        var functionSummary = _basicArithmetic.Subtract(3, -45);

        functionSummary.Result.Should().Be(48);
    }

    [Fact]
    public void SubtractingSmallPositiveFromBigNegativeIntegers()
    {
        var functionSummary = _basicArithmetic.Subtract(-45, 3);

        functionSummary.Result.Should().Be(-48);
    }

    [Fact]
    public void SubstractingNegativeIntegersStartingWithSmallNegative()
    {
        var functionSummary = _basicArithmetic.Subtract(-3, -45);

        functionSummary.Result.Should().Be(42);
    }

    [Fact]
    public void SubstractingNegativeIntegersStartingWithBigNegative()
    {
        var functionSummary = _basicArithmetic.Subtract(-45, -3);

        functionSummary.Result.Should().Be(-42);
    }
}
