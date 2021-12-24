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
        private TokenType _previusTokenType = TokenType.None;
        private char _previusToken;

        public DeterminerService(Operations operations)
        {
            _operationsList = operations.OperationsList;
        }

        public TokenType DetermineTokenType(char token)
        {
            _previusToken = token;
            if (token is '(' or ')') _previusTokenType = TokenType.Parentheses;
            else if (token is ',') _previusTokenType = TokenType.Comma;
            else if (_operationsList.Contains(token))
            {
                if(token is '-' && (_previusTokenType is TokenType.Operation or TokenType.None || _previusToken is'('))
                    _previusTokenType = TokenType.Number;
                else _previusTokenType = TokenType.Operation;
            }
            if (char.IsDigit(token)) _previusTokenType = TokenType.Number;
            return _previusTokenType;
        }
    }
}
