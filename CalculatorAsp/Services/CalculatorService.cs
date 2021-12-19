using CalculatorAsp.Models;

namespace CalculatorAsp.Services;

public interface ICalculatorService
{
    string Calculate(IEnumerable<Token> reversPolishNotice);
}

public class CalculatorServiceService : ICalculatorService
{
    private readonly Dictionary<string, Func<double, double, double>> _operationsMap;
    public CalculatorServiceService(Operations operations)
    {
        _operationsMap = operations.OperationsMap;
    }

    public string Calculate(IEnumerable<Token> reversPolishNotice)
    {
        var stack = new Stack<double>();
        foreach (var token in reversPolishNotice)
            stack.Push(token.Type == TokenType.Number
                ? double.Parse(token.Content)
                : _operationsMap[token.Content](stack.Pop(), stack.Pop()));
        return stack.Pop().ToString();
    }
}