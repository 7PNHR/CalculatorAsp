using System.Text;
using CalculatorAsp.Models;

namespace CalculatorAsp.Services;

public interface ITokenizerService
{
    IEnumerable<Token> GetTokens(string inputLine);
}

public class TokenizerServiceService : ITokenizerService
{
    private readonly IDeterminerService _determinerService;

    public TokenizerServiceService(IDeterminerService determinerService)
    {
        _determinerService = determinerService;
    }

    public IEnumerable<Token> GetTokens(string inputLine)
    {
        var sb = new StringBuilder();
        var lastTokenType = TokenType.None;
        foreach (var token in inputLine.Where(x => x != ' ' || !char.IsWhiteSpace(x)))
        {
            var tokenType = _determinerService.DetermineTokenType(token);
            if (tokenType is not TokenType.Number and not TokenType.Comma)
            {
                if (sb.Length! > 0) yield return new Token(lastTokenType, sb.ToString());
                yield return new Token(tokenType, token.ToString());
                sb.Clear();
            }
            else sb.Append(token);

            lastTokenType = tokenType;
        }

        if (sb.Length > 0) yield return new Token(lastTokenType, sb.ToString());
    }
}