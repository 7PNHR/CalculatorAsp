namespace CalculatorAsp.Models;

public class Operations
{
    public List<char> OperationsList = new()
    {
        '+',
        '-',
        '*',
        '/',
    };

    public readonly Dictionary<string, Func<double, double, double>> OperationsMap = new()
    {
        { "+", (y, x) => x + y },
        { "-", (y, x) => x - y },
        { "/", (y, x) => x / y },
        { "*", (y, x) => x * y },
    };

    public readonly Dictionary<string, int> PrioritiesMap = new()
    {
        { "+", 2 },
        { "-", 2 },
        { "/", 1 },
        { "*", 1 }
    };
}