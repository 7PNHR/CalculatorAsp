using CalculatorAsp.Models;

namespace CalculatorAsp.Services
{
    public interface IDeterminerService
    {
        TokenType DetermineTokenType(char token);
    }

    public class DeterminerService : IDeterminerService
    {
        private readonly List<char> _operationsList;

        public DeterminerService(Operations operations)
        {
            _operationsList = operations.OperationsList;
        }

        public TokenType DetermineTokenType(char token)
        {
            if (token is '(' or ')') return TokenType.Parentheses;
            if (token is ',') return TokenType.Comma;
            if (_operationsList.Contains(token)) return TokenType.Operation;
            if (char.IsDigit(token)) return TokenType.Number;
            throw new Exception("Unrecognizable token type");
        }
    }
}
