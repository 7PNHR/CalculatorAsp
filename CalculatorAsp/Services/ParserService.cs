using CalculatorAsp.Models;

namespace CalculatorAsp.Services;

public interface IParserService
{
    IEnumerable<Token> ParseToReversePolishNotation(IEnumerable<Token> tokens);
}

public class ParserServiceService : IParserService
{
    private readonly Dictionary<string, int> _prioritiesMap;

    public ParserServiceService(Operations operations)
    {
        _prioritiesMap = operations.PrioritiesMap;
    }

    public IEnumerable<Token> ParseToReversePolishNotation(IEnumerable<Token> tokens)
    {
        var stack = new Stack<Token>();
        foreach (var token in tokens)
        {
            switch (token.Type)
            {
                case TokenType.Number:
                    yield return token;
                    break;
                case TokenType.Operation:
                    while (stack.Count > 0 && stack.Peek().Type == TokenType.Operation &&
                           _prioritiesMap[token.Content] >= _prioritiesMap[stack.Peek().Content])
                        yield return stack.Pop();
                    stack.Push(token);
                    break;
                case TokenType.Parentheses:

                    if (token.Content.Equals("("))
                        stack.Push(token);
                    else
                    {
                        while (!stack.Peek().Content.Equals("("))
                            yield return stack.Pop();
                        stack.Pop();
                    }

                    break;
            }
        }

        while (stack.Count > 0) yield return stack.Pop();
    }
}