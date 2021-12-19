namespace CalculatorAsp.Models;

public enum TokenType
{
    None,
    Number,
    Operation,
    Parentheses,
    Comma
}

public class Token
{
    public TokenType Type { get; set; }
    public string Content { get; set; }

    public Token(TokenType type, string content)
    {
        Type = type;
        Content = content;
    }

}