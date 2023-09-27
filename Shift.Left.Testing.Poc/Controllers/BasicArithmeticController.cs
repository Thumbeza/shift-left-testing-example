using Shift.Left.Testing.Poc.Models;

namespace Shift.Left.Testing.Poc.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BasicArithmeticController : ControllerBase
{
    private readonly ILogger<BasicArithmeticController> _logger;

    public BasicArithmeticController(ILogger<BasicArithmeticController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Add")]
    public FunctionSummary Add(int firstNumbber, int secondNumber)
    {
        var result = firstNumbber + secondNumber;

        _logger.LogInformation($"Adding {secondNumber} onto {firstNumbber} resulted to {result}");

        return new FunctionSummary
        {
            FirstNumber = firstNumbber,
            SecondNumber = secondNumber,
            Result = result,
            Type = "Addition"
        };
    }

    [HttpGet(Name = "Subtract")]
    public FunctionSummary Subtract(int firstNumbber, int secondNumber)
    {
        var result = firstNumbber - secondNumber;

        _logger.LogInformation($"Subtracting {secondNumber} from {firstNumbber} resulted to {result}");

        return new FunctionSummary
        {
            FirstNumber = firstNumbber,
            SecondNumber = secondNumber,
            Result = result,
            Type = "Subtraction"
        };
    }

    //TODO: add (division and multiplication) endpoints via the pr process,
    //upon enforcing the pr policy to include code coverage quality gate, required approval from QA group, all unit tests and Post Deployment Tests (PDTs) are passing
}
