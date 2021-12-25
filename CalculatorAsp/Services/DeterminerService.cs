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
            var tokenType = TokenType.None;
            _previusToken = token;
            if (token is '(' or ')') tokenType = TokenType.Parentheses;
            else if (token is ',') tokenType = TokenType.Comma;
            else if (_operationsList.Contains(token))
            {
                if(token is '-' && (_previusTokenType is TokenType.Operation or TokenType.None || _previusToken is'('))
                    tokenType = TokenType.Number;
                else tokenType = TokenType.Operation;
            }
            if (char.IsDigit(token)) tokenType = TokenType.Number;
            if (tokenType == TokenType.None)
                throw new Exception();
            _previusTokenType = tokenType;
            return tokenType;
        }
    }
}
